using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.OrderDTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(IGenericRepository<Order> repository, IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork, ICartRepository cartRepository, IOrderDetailService orderDetailService, IHttpContextAccessor httpContextAccessor) : base(repository, unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
            _orderDetailService = orderDetailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomResponseDto<List<OrderDto>>> GetOrderWithDetailByUserId(int id)
        {
            var orders = await _orderRepository.GetOrderWithDetailByUserId(id);
            var request = _httpContextAccessor.HttpContext.Request;
            var userOrders = orders.Select(x => new OrderDto
            {
                Id = x.Id,
                UserId = (int)x.UserId,
                Status = x.Status,
                Total = x.Total,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                User = new UserDto { Id = x.User.Id, Email = x.User.Email, Name = x.User.Name, Surname = x.User.Surname, Phone = x.User.Phone, },
                OrderDetail = x.OrderDetails.Select(y => new OrderDetailDto
                {
                    Id = y.Id,
                    ProductId = y.ProductFeature.ProductId,
                    ProductName = y.ProductFeature.Product.Name,
                    Color = y.ProductFeature.Color,
                    ProductPrice = y.ProductFeature.FePrice,
                    Quantity = y.Quantity,
                    Path =  request.Scheme + "://" + request.Host.Value + "/img/product/" + y.ProductFeature.Product.ProductImages.FirstOrDefault().Path,
                    Status = y.Status,
                    Total = y.Quantity * y.ProductFeature.FePrice
                }).ToList(),

            }).ToList();

            return CustomResponseDto<List<OrderDto>>.Success(200, userOrders);
        }

        public async Task<CustomResponseDto<List<OrderDto>>> GetUndeletedOrders()
        {
            var orders = await _orderRepository.GetUndeletedOrders();
            var request = _httpContextAccessor.HttpContext.Request;
            var userOrders = orders.Select(x => new OrderDto
            {
                Id = x.Id,
                UserId = (int)x.UserId,
                Status = x.Status,
                Total = x.Total,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                User = new UserDto { Id = x.User.Id, Email = x.User.Email, Name = x.User.Name, Surname = x.User.Surname, Phone = x.User.Phone, },
                OrderDetail = x.OrderDetails.Select(y => new OrderDetailDto
                {
                    Id = y.Id,
                    ProductId = y.ProductFeature.ProductId,
                    ProductName = y.ProductFeature.Product.Name,
                    Color = y.ProductFeature.Color,
                    ProductPrice = y.ProductFeature.FePrice,
                    Quantity = y.Quantity,
                    Path = request.Scheme + "://" + request.Host.Value + "/img/product/" + y.ProductFeature.Product.ProductImages.FirstOrDefault().Path,
                    Status = y.Status,
                    Total = y.Quantity * y.ProductFeature.FePrice
                }).ToList(),

            }).ToList();

            return CustomResponseDto<List<OrderDto>>.Success(200, userOrders);
        }

        public async Task<CustomResponseDto<NoContentDto>> SaveOrder(int userid)
        {
            var userCart = await _cartRepository.GetCartWithUserId(userid);
            decimal total = 0;
            Order order = new Order();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            if(userCart != null && userCart.Count() > 0)
            {
                foreach(var cart in userCart)
                {
                    total += cart.Quantity * cart.ProductFeature.FePrice;       
                }
                order.UserId = userid;
                order.Total = total;
                order.Status = "Sipariş Beklemede";
                order.IsActive = true;
                order.CreatedDate = DateTime.Now;
                await AddAsync(order);

                foreach(var cart in userCart)
                {
                    orderDetails.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductFeatureId = cart.ProductFeatureId,
                        Quantity = cart.Quantity,
                        Price = cart.Quantity * cart.ProductFeature.FePrice,
                        Status = order.Status,
                        CreatedDate = DateTime.Now,
                    });
                }
                await _orderDetailService.AddRangeAsync(orderDetails);
            }

            return CustomResponseDto<NoContentDto>.Success(200, "Sipariş Oluşturuldu");
        }
    }
}
