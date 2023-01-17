using EisntLivros.DataAccess.Repository.IRepository;
using EisntLivros.Models;
using EisntLivros.Models.ViewModels;
using EisntLivros.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EisntLivrosWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; } = null!;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(_ => _.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(_ => _.OrderId == orderId, includeProperties: "Product")
            };

            return View(OrderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> orderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity!;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitOfWork.OrderHeader.GetAll(_ => _.ApplicationUserId == claims!.Value, includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(_ => _.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;

                case "inprocess":
                    orderHeaders = orderHeaders.Where(_ => _.OrderStatus == SD.StatusInProcess);
                    break;

                case "completed":
                    orderHeaders = orderHeaders.Where(_ => _.OrderStatus == SD.StatusShipped);
                    break;

                case "approved":
                    orderHeaders = orderHeaders.Where(_ => _.OrderStatus == SD.StatusApproved);
                    break;
            }

            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}
