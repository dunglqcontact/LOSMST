﻿using LOSMST.Business.Service;
using LOSMST.Models.Database;
using LOSMST.Models.Helper;
using LOSMST.Models.Helper.DBOHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LOSMST.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductParameter productParam, [FromQuery] PagingParameter paging)
        {
            var data = _productService.GetAllProducts(productParam, paging);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (_productService.Add(product))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {

            if (_productService.Update(product))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
