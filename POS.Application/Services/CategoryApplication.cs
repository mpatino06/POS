using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;

namespace POS.Application.Services;

public class CategoryApplication : ICategoryApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly CategoryValidator _validationRules;
    public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationRules = validationRules;
    }

    public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters)
    {
        var response = new BaseResponse<BaseEntityResponse<CategoryResponseDto>>();
        var categories = await _unitOfWork.Category.ListCategories(filters);

        if (categories is not null)
        {
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
        }
        else
        {
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            response.Message = "No se encontraron categorías.";
        }

        return response;
    }

    public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
    {
        var response = new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
        var categories = await _unitOfWork.Category.GetAllAsync();

        if (categories is not null)
        {
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
        }
        else
        {
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            response.Message = "No se encontraron categorías.";
        }

        return response;
    }

    public async Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId)
    {
        var category = await _unitOfWork.Category.GetByIdAsync(categoryId);

        if (category is null)
        {
            return new BaseResponse<CategoryResponseDto>
            {
                IsSuccess = false,
                Message = $"{ReplyMessage.MESSAGE_QUERY_EMPTY} No se encontró la categoría."
            };
        }

        return new BaseResponse<CategoryResponseDto>
        {
            IsSuccess = true,
            Message = ReplyMessage.MESSAGE_QUERY,
            Data = _mapper.Map<CategoryResponseDto>(category)
        };
    }

    public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var category = _mapper.Map<Category>(requestDto);

        response.Data = await _unitOfWork.Category.RegisterAsync(category);

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        else
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }


    public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var categoryExists = await CategoryById(categoryId);

        if (categoryExists.Data is null)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
        }

        var category = _mapper.Map<Category>(requestDto);
        category.Id = categoryId;
        response.Data = await _unitOfWork.Category.EditAsync(category);
        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        else
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        return response;    
    }

    public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
    {
        var response = new BaseResponse<bool>();
        var categoryExists = await CategoryById(categoryId);

        if (categoryExists.Data is null)
        {
            return new BaseResponse<bool>
            {
                IsSuccess = false,
                Message = ReplyMessage.MESSAGE_QUERY_EMPTY
            };
        }

        response.Data = await _unitOfWork.Category.RemoveAsync(categoryId);

        if (response.Data)
        {
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        else
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }

        return response;
    }
}
