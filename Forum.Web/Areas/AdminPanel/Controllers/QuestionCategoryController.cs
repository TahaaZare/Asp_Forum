using Forum.Application.Services.Interfaces.Question;
using Forum.Domain.Enums.Admin.Question.Category;
using Forum.Domain.ViewModels.Question.Category;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[PermissionChecker("Manage QuestionCategories")]
public class QuestionCategoryController : AdminBaseController
{
    #region ctor

    private IQuestionCategoryService _questionCategoryService;

    public QuestionCategoryController(IQuestionCategoryService questionCategoryService)
    {
        _questionCategoryService = questionCategoryService;
    }

    #endregion

    public async Task<IActionResult> Index()
    {
        var categories = await _questionCategoryService.GetAllQuestionCategories();
        return View(categories);
    }

    #region create

    [HttpGet("create-q-category")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost("create-q-category")]
    public async Task<IActionResult> Create(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _questionCategoryService.CreateCategory(model);
        switch (result)
        {
            case CreateCategoryResult.Success:
                TempData[SuccessMessage] = "دسته بندی با موفقیت اضافه شد !";
                return RedirectToAction("Index", "QuestionCategory", new { area = "AdminPanel" });
            case CreateCategoryResult.CategoryNameExist:
                TempData[WarningMessage] = "نام وارد شده قبلا ثبت شده است !";
                break;
        }

        return View(model);
    }

    #endregion

    #region edit

    [HttpGet("edit-qc/{name}")]
    public async Task<IActionResult> Edit(string name)
    {
        var category = await _questionCategoryService.FillEditCategoryViewModel(name);

        return View(category);
    }

    [HttpPost("edit-qc/{name}")]
    public async Task<IActionResult> Edit(EditCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _questionCategoryService.EditCategory(model);
        switch (result)
        {
            case EditCategoryQuestionResult.Success:
                TempData[SuccessMessage] = "دسته بندی با موفقیت ویرایش شد !";
                return RedirectToAction("Index", "QuestionCategory", new { area = "AdminPanel" });
            case EditCategoryQuestionResult.CategoryNameExist:
                TempData[WarningMessage] = "نام وارد شده قبلا ثبت شده است !";
                break;
            case EditCategoryQuestionResult.NotFound:
                TempData[ErrorMessage] = "خطا !";
                break;
        }

        return View(model);
    }

    #endregion

    #region delete

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        var category = await _questionCategoryService.GetQuestionCategoryByName(name);
        await _questionCategoryService.DeleteCategory(category);
        
        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد !";
        return RedirectToAction("Index", "QuestionCategory", new { area = "AdminPanel" });
    }
    

    #endregion
}