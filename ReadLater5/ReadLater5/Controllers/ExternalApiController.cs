using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.Net.Http.Headers;
using ReadLater5.JWT;
using Microsoft.Extensions.Configuration;
using Services;

namespace ReadLater5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalApiController : ControllerBase
    {
        private ValidateJwt _validate;
        private IExternalBookmarkService _IbookmarkService;
        public ExternalApiController(IConfiguration configuration, IExternalBookmarkService service)
        {
            _IbookmarkService = service;
            _validate = new ValidateJwt(configuration);
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (!_validate.Validate(Request.Headers[HeaderNames.Authorization].ToString()))
            {
                return BadRequest();
            }
            return Ok(_IbookmarkService.GetBookmarks());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (!_validate.Validate(Request.Headers[HeaderNames.Authorization].ToString()))
            {
                return BadRequest();
            }
            return Ok(_IbookmarkService.GetBookmark(id));
        }

        [HttpPost]
        public ActionResult Post(Bookmark bookmark)
        {
            if (!_validate.Validate(Request.Headers[HeaderNames.Authorization].ToString()))
            {
                return BadRequest();
            }
            return Ok(_IbookmarkService.CreateBookmark(bookmark));
        }

        [HttpPut]
        public ActionResult Put(Bookmark bookmark)
        {
            if (bookmark.ID == 0)
            {
                return NotFound("bookmark not found!");
            }

            _IbookmarkService.UpdateBookmark(bookmark);

            return Ok(bookmark);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound("bookmark not found");
            }

            _IbookmarkService.DeleteBookmark(_IbookmarkService.GetBookmark(id));

            return Ok("Deleted!");
        }
    }
}
