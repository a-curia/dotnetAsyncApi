using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Filters
{
    // chnage the book entity on how it's send to the consumer
    public class BookResultFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult; // downcasting
            await next();
            
            if(resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300) // we only filter status 200 and not null, otherwise go next
            {
                await next();
                return;
            }

            //if (typeof(IEnumerable).IsAssignableFrom(resultFromAction.Value.GetType()))
            //{
            //    // we are dealing with an IEnumerable and we can check in here but this violetes the single responsability principale - so create BooksResultFilterAttribute
            //}

            // resultFromAction.Value  - this is not good, so add support for automapper
            resultFromAction.Value = Mapper.Map<Models.Book>(resultFromAction.Value);


            await next();
        }
    }
}
