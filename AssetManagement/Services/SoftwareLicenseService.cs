using FluentValidation;
using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Mappers;
using AssetManagementApi.Repositories;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public class SoftwareLicenseService : ISoftwareLicenseService
{
    private readonly ISoftwareLicenseRepository _licenseRepository;
    private readonly ISoftwareLicenseMapper _licenseMapper;
    private readonly IValidator<SoftwareLicenseCreateUpdateDto> _validator;

    public SoftwareLicenseService(
        ISoftwareLicenseRepository licenseRepository,
        ISoftwareLicenseMapper licenseMapper,
        IValidator<SoftwareLicenseCreateUpdateDto> validator)
    {
        _licenseRepository = licenseRepository;
        _licenseMapper = licenseMapper;
        _validator = validator;
    }

    public async Task<IReadOnlyCollection<SoftwareLicenseDto>> GetSoftwareLicensesAsync()
    {
        var licenses = await _licenseRepository.GetAllAsync();
        return licenses.Select(_licenseMapper.ToDto).ToList();
    }

    public async Task<SoftwareLicenseDetailDto?> GetSoftwareLicenseAsync(int id)
    {
        var license = await _licenseRepository.GetDetailAsync(id);
        return license is null ? null : _licenseMapper.ToDetailDto(license);
    }

    public async Task<OperationResult<SoftwareLicenseDto>> CreateSoftwareLicenseAsync(SoftwareLicenseCreateUpdateDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult<SoftwareLicenseDto>.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        var license = _licenseMapper.ToEntity(dto);
        await _licenseRepository.CreateAsync(license);
        await _licenseRepository.SaveChangesAsync();

        return OperationResult<SoftwareLicenseDto>.Success(_licenseMapper.ToDto(license));
    }

    public async Task<OperationResult> UpdateSoftwareLicenseAsync(int id, SoftwareLicenseCreateUpdateDto dto)
    {
        var license = await _licenseRepository.GetByIdAsync(id);
        if (license is null)
        {
            return OperationResult.NotFound();
        }

        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        _licenseMapper.UpdateEntity(license, dto);
        await _licenseRepository.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteSoftwareLicenseAsync(int id)
    {
        var license = await _licenseRepository.GetByIdAsync(id);
        if (license is null)
        {
            return OperationResult.NotFound();
        }

        await _licenseRepository.DeleteAsync(id);
        await _licenseRepository.SaveChangesAsync();

        return OperationResult.Success();
    }
}
