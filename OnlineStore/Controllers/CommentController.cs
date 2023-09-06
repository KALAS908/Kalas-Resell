using Microsoft.AspNetCore.Mvc;
using OnlineStore.BusinessLogic.Implementation.ComentsImplementation;
using OnlineStore.Code;
using OnlineStore.WebApp.Code;

namespace OnlineStore.WebApp.Controllers
{
    public class CommentController : BaseController
    {
        public CommentService CommentService;
        public CommentController(ControllerDependencies dependencies, CommentService commentService) : base(dependencies)
        {
            CommentService = commentService;
        }

        [HttpGet]
        public IActionResult GetProductComments( Guid productId)
        {
            return Ok();
        }
    }
}
