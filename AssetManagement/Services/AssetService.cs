using FluentValidation;
using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Mappers;
using AssetManagementApi.Models;
using AssetManagementApi.Repositories;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IAssetMapper _assetMapper;
    private readonly IValidator<AssetCreateDto> _createValidator;
    private readonly IValidator<AssetUpdateDto> _updateValidator;

    public AssetService(
        IAssetRepository assetRepository,
        IAssetMapper assetMapper,
        IValidator<AssetCreateDto> createValidator,
        IValidator<AssetUpdateDto> updateValidator)
    {
        _assetRepository = assetRepository;
        _assetMapper = assetMapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IReadOnlyCollection<AssetDto>> GetAssetsAsync()
    {
        var assets = await _assetRepository.GetAllAsync();
        return assets.Select(_assetMapper.ToDto).ToList();
    }

    public async Task<AssetDetailDto?> GetAssetAsync(int id)
    {
        var asset = await _assetRepository.GetDetailAsync(id);
        if (asset is null || asset.WarrantyCard is null)
        {
            return null;
        }

        return _assetMapper.ToDetailDto(asset);
    }

    public async Task<OperationResult<AssetDetailDto>> CreateAssetAsync(AssetCreateDto dto)
    {
        var validation = await _createValidator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult<AssetDetailDto>.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        var asset = _assetMapper.ToEntity(dto);
        await _assetRepository.CreateAsync(asset);
        await _assetRepository.SaveChangesAsync();

        var created = await _assetRepository.GetDetailAsync(asset.Id);
        if (created is null || created.WarrantyCard is null)
        {
            return OperationResult<AssetDetailDto>.NotFound();
        }

        return OperationResult<AssetDetailDto>.Success(_assetMapper.ToDetailDto(created));
    }

    public async Task<OperationResult> UpdateAssetAsync(int id, AssetUpdateDto dto)
    {
        var asset = await _assetRepository.GetForUpdateAsync(id);
        if (asset is null || asset.WarrantyCard is null)
        {
            return OperationResult.NotFound();
        }

        var validation = await _updateValidator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        _assetMapper.UpdateEntity(asset, dto);
        await _assetRepository.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAssetAsync(int id)
    {
        var asset = await _assetRepository.GetForDeleteAsync(id);
        if (asset is null)
        {
            return OperationResult.NotFound();
        }

        await _assetRepository.DeleteAsync(id);
        await _assetRepository.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> AssignLicenseAsync(int assetId, int licenseId)
    {
        var assetExists = await _assetRepository.AssetExistsAsync(assetId);
        var licenseExists = await _assetRepository.SoftwareLicenseExistsAsync(licenseId);

        if (!assetExists || !licenseExists)
        {
            return OperationResult.NotFound();
        }

        var existing = await _assetRepository.FindAssetSoftwareLicenseAsync(assetId, licenseId);
        if (existing is null)
        {
            await _assetRepository.AddAssetSoftwareLicenseAsync(new AssetSoftwareLicense
            {
                AssetId = assetId,
                SoftwareLicenseId = licenseId
            });
            await _assetRepository.SaveChangesAsync();
        }

        return OperationResult.Success();
    }

    public async Task<OperationResult> RemoveLicenseAsync(int assetId, int licenseId)
    {
        var link = await _assetRepository.FindAssetSoftwareLicenseAsync(assetId, licenseId);
        if (link is null)
        {
            return OperationResult.NotFound();
        }

        await _assetRepository.RemoveAssetSoftwareLicenseAsync(link);
        await _assetRepository.SaveChangesAsync();

        return OperationResult.Success();
    }
}
