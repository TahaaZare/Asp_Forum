using Forum.Application.Services.Interfaces.Tag;
using Forum.Domain.Models.Tags;
using Forum.Web.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AdminPanel.Controllers;

[PermissionChecker("Manage Tags")]
public class AdminTagController : AdminBaseController
{
    #region ctor

    private ITagService _tagService;

    public AdminTagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    #endregion

    public async Task<IActionResult> Index()
    {
        var tags = await _tagService.GetTagListAdminPanel();
        var tag = new Tag();
        ViewData["tag"] = tag;
        
        return View(tags);
    }

    [HttpPost("store-tag"),ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tag tag)
    {
        await _tagService.CreateTagFromAdminPanel(tag);
        await _tagService.SaveChanges();
        TempData[SuccessMessage] = "تگ مورد نظر با موفقیت اضافه شد";
        return RedirectToAction("Index", "AdminTag", new { area = "AdminPanel" });
    }
    
    // [HttpPost("update-tag"),ValidateAntiForgeryToken]
    // public async Task<IActionResult> Update(long id)
    // {
    //     var tag = await _tagService.GetTagById(id);
    //     await _tagService.UpdateTagFromAdminPanel(tag);
    //     await _tagService.SaveChanges();
    //     TempData[SuccessMessage] = "تگ مورد نظر با موفقیت ویرایش شد";
    //     return RedirectToAction("Index", "AdminTag", new { area = "AdminPanel" });
    // }
    
    [HttpPost("delete-tag/{id}"),ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTag(long id)
    {
        var tag = await _tagService.GetTagById(id);
        await _tagService.DeleteTagFromAdminPanel(tag);
        await _tagService.SaveChanges();
        TempData[SuccessMessage] = "تگ مورد نظر با موفقیت حذفـ شد";
        return RedirectToAction("Index", "AdminTag", new { area = "AdminPanel" });
    }
}