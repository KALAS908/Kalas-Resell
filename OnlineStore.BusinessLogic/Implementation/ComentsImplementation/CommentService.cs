using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Base;
using OnlineStore.Common.DTOs;
using OnlineStore.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Implementation.ComentsImplementation
{
    public class CommentService : BaseService
    {
        public CommentService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public void AddComment(CommentDto model)
        {
            var comment = new Comment
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                Text = model.Text,
                PostDate = DateTime.FromOADate(DateTime.Now.ToOADate()),
                Rating = model.Rating
            };
            UnitOfWork.Comments.Insert(comment);
            UnitOfWork.SaveChanges();
        }

        public List<CommentDto> GetProductComments(Guid productId)
        {
            List<Comment> result = UnitOfWork.Comments.Get()
                .Include(x => x.User)
                .Where(x => x.ProductId == productId)
                .ToList();
            List<CommentDto> resultDto = new List<CommentDto>();
            foreach (var comment in result)
            {
                resultDto.Add(new CommentDto
                {
                    Id = comment.Id,
                    ProductId = (Guid)comment.ProductId,
                    UserId = (Guid)comment.UserId,
                    UserName = comment.User.UserName,
                    Text = comment.Text,
                    Date = (DateTime)comment.PostDate,
                    Rating = (int)comment.Rating
                });
            }
            return resultDto;
        }

        public void RemoveComment(int commentId)
        {
            var comment = UnitOfWork.Comments.Get()
                .FirstOrDefault(x => x.Id == commentId);

            UnitOfWork.Comments.Delete(comment);
            UnitOfWork.SaveChanges();
        }
    }
}
