using FluentValidation;
using AssetManagementApi.Repositories;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public abstract class GenericService<TEntity, TDto, TCreateUpdateDto>
    where TEntity : class
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IValidator<TCreateUpdateDto> _validator;

    protected GenericService(
        IGenericRepository<TEntity> repository,
        IValidator<TCreateUpdateDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    protected virtual Task<TEntity?> GetByIdForReadAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyCollection<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto).ToList();
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await GetByIdForReadAsync(id);
        return entity is null ? default : MapToDto(entity);
    }

    public async Task<OperationResult<TDto>> CreateAsync(TCreateUpdateDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult<TDto>.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        var entity = MapToEntity(dto);
        await _repository.CreateAsync(entity);
        await _repository.SaveChangesAsync();

        return OperationResult<TDto>.Success(MapToDto(entity));
    }

    public async Task<OperationResult> UpdateAsync(int id, TCreateUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return OperationResult.NotFound();
        }

        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return OperationResult.ValidationFailed(
                validation.Errors.Select(error => error.ErrorMessage).ToList());
        }

        UpdateEntity(entity, dto);
        await _repository.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return OperationResult.NotFound();
        }

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return OperationResult.Success();
    }

    protected abstract TDto MapToDto(TEntity entity);
    protected abstract TEntity MapToEntity(TCreateUpdateDto dto);
    protected abstract void UpdateEntity(TEntity entity, TCreateUpdateDto dto);
}
