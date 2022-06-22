using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.OrderDTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class OrdersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrdersController(IMapper mapper, IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPendingOrderWithDetailByUserId(int id)
        {
            return CreateActionResult(await _orderService.GetPendingOrderWithDetailByUserId(id));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompletedOrderWithDetailByUserId(int id)
        {
            return CreateActionResult(await _orderService.GetCompletedOrderWithDetailByUserId(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUndeletedPendingOrders()
        {
            return CreateActionResult(await _orderService.GetUndeletedPendingOrders());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUndeletedCompletedOrders()
        {
            return CreateActionResult(await _orderService.GetUndeletedCompletedOrders());
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(int userid)
        {
            return CreateActionResult(await _orderService.SaveOrder(userid));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var Orders = await _orderService.GetAllAsync();
            var OrderDtos = _mapper.Map<List<OrderDto>>(Orders.ToList());

            return CreateActionResult(CustomResponseDto<List<OrderDto>>.Success(200, OrderDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Order>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Order = await _orderService.GetByIdAsync(id);
            var OrderDto = _mapper.Map<OrderDto>(Order);

            return CreateActionResult(CustomResponseDto<OrderDto>.Success(200, OrderDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderDto OrderDto)
        {
            await _orderService.UpdateAsync(_mapper.Map<Order>(OrderDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpPatch("{id}")]

        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument Order)
        {
            await _orderService.UpdatePatchAsync(id, Order);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var Order = await _orderService.GetByIdAsync(id);

            await _orderService.RemoveAsync(Order);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
