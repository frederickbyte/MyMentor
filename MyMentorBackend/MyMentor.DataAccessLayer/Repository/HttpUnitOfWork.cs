using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using MyMentor.DataAccessLayer.Auth;

namespace MyMentor.DataAccessLayer.Repository
{
    public class HttpUnitOfWork : UnitOfWork
    {
        public HttpUnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpAccessor) : base(context)
        {
            context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst(ClaimConstants.Subject)?.Value?.Trim();
        }
    }
}
