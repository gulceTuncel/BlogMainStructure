using BlogMainStructure.Business.Services.AuthorServices;
using BlogMainStructure.Business.Services.ArticleServices;
using BlogMainStructure.Business.Services.TagServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Mapster;
using BlogMainStructure.UI.Areas.Author.Models.ArticleVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogMainStructure.Business.DTOs.ArticleDTOs;
using BlogMainStructure.UI.Extensions;

namespace BlogMainStructure.UI.Areas.Author.Controllers
{
    public class ArticleController : AuthorBaseController
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorService _authorService;
        private readonly ITagService _tagService;
        public ArticleController(IArticleService articleService, UserManager<IdentityUser> userManager, IAuthorService authorService, ITagService tagService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _authorService = authorService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var authorId = await _authorService.GetAuthorIdByEmail(userEmail);
            var result = await _articleService.GetAllAsync(authorId);
            var articleListVMs = result.Data.Adapt<List<AuthorArticleListVM>>();

            foreach(var articleListVM in articleListVMs)
            {
                articleListVM.ReadingTime = await articleListVM.Content.CalcualteReadingTime();
            }

            if (!result.IsSuccess)
            {
                return View(result.Data.Adapt<List<AuthorArticleListVM>>());
            }
            return View(result.Data.Adapt<List<AuthorArticleListVM>>());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _articleService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return View(result.Data.Adapt<AuthorArticleDetailVM>());
        }

        public async Task<IActionResult> Create()
        {
            AuthorArticleCreateVM authorArticleCreateVM = new AuthorArticleCreateVM()
            {
                Tags = await GetTags()
            };


            return View(authorArticleCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorArticleCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Tags = await GetTags();
                return View(model);
            }

            var userMail = User.FindFirstValue(ClaimTypes.Email);
            var authorId = await _authorService.GetAuthorIdByEmail(userMail);

            model.AuthorId = authorId;
            var articleCreateDTO = model.Adapt<ArticleCreateDTO>();

            if(model.NewImage != null && model.NewImage.Length > 0)
            {
                articleCreateDTO.Image = await model.NewImage.StringToByteArrayAsync();
            }

            //articleCreateDTO.Content = (await articleCreateDTO.Content.FormatTextAreaAsync()).ToHtmlString();

            var result = await _articleService.AddAsync(articleCreateDTO);

            if (!result.IsSuccess)
            {
                model.Tags = await GetTags();
                return View(model);
            }

            return RedirectToAction("Index");
        }


        private async Task<SelectList> GetTags(Guid? tagId = null)
        {
            var tags = (await _tagService.GetAllAsync()).Data;

            return new SelectList(tags.Select(src => new SelectListItem
            {
                Value = src.Id.ToString(),
                Text = src.Name,
                Selected = src.Id == (tagId != null ? tagId.Value : tagId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }
    }
}
