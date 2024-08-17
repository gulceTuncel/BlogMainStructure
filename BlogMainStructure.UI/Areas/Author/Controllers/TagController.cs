using BlogMainStructure.Business.DTOs.TagDTOs;
using BlogMainStructure.Business.Services.TagServices;
using BlogMainStructure.UI.Areas.Author.Models.TagVMs;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BlogMainStructure.UI.Areas.Author.Controllers
{
    public class TagController : AuthorBaseController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _tagService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return View(result.Data.Adapt<List<AuthorTagListVM>>());
            }
            return View(result.Data.Adapt<List<AuthorTagListVM>>());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorTagCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _tagService.AddAsync(model.Adapt<TagCreateDTO>());
            if (!result.IsSuccess)
            {
                return View(model);

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tagService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
    }
}