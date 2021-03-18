using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeTravel.ServiceInterface;
using WeTravel.Model;

namespace WeTravel.WebApi.Controllers
{
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Create([FromBody] ReviewModelIn model)
        {
            _reviewService.Create(model);
            return Ok();
        }
    }
}
