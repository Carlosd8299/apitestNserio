using Domain.Models;
using Infraestructure.Dtos.Request;
using Infraestructure.Dtos.Response;
using Infraestructure.Helpers;
using Infraestructure.Interfaces;
using Infraestructure.Setings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class OrdersRepository : SqlServerBase<Order>, IOrdersRepository
    {
        private readonly string _spGetClientOrders;
        private readonly string _spCreateOrder;
        public OrdersRepository(
        IOptions<InfraestructureSettings> settings
         ) : base(settings.Value.SqlSettings.ConnectionString)
        {
            _spGetClientOrders = settings.Value.SqlSettings.SpGetClientOrders;
            _spCreateOrder = settings.Value.SqlSettings.SpAddNewOrder;
        }

        public async Task<List<GetOrdersByClientResponseDTO>> GetOrdersByClient(int idCustomer)
        {
            List<GetOrdersByClientResponseDTO> response = new();

            try
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@custid", SqlDbType.Int, 128) { Value = idCustomer } };

                using (var rdr = await ExecuteSpResults(_spGetClientOrders, parameters))
                {

                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {
                        var item = new GetOrdersByClientResponseDTO
                        {
                            Orderid = Convert.ToInt32(rdr.Tables[0].Rows[i]["orderid"]),
                            Shipaddress = Convert.ToString(rdr.Tables[0].Rows[i]["shipaddress"]),
                            Shipname = Convert.ToString(rdr.Tables[0].Rows[i]["shipname"]),
                            Shipcity = Convert.ToString(rdr.Tables[0].Rows[i]["shipcity"]),
                            Requireddate = Convert.ToDateTime(rdr.Tables[0].Rows[i]["requireddate"]),
                            Shippeddate = Convert.ToDateTime(rdr.Tables[0].Rows[i]["shippeddate"]),
                        };
                        response.Add(item);
                    }

                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CreateOrderResponseDto> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
        {
            CreateOrderResponseDto createOrderResponseDto = new();
            try
            {
                List<SqlParameter> parameters = new();

                parameters.Add(new SqlParameter("@empid", SqlDbType.Int, 128) { Value = createOrderRequestDto.empid });
                parameters.Add(new SqlParameter("@productid", SqlDbType.Int, 128) { Value = createOrderRequestDto.productid });
                parameters.Add(new SqlParameter("@shipperid", SqlDbType.Int, 128) { Value = createOrderRequestDto.shipperid });
                parameters.Add(new SqlParameter("@qty", SqlDbType.SmallInt, 128) { Value = createOrderRequestDto.qty });
                parameters.Add(new SqlParameter("@discount", SqlDbType.Decimal) { Value = createOrderRequestDto.discount });
                parameters.Add(new SqlParameter("@unitprice", SqlDbType.Money) { Value = createOrderRequestDto.unitprice });
                parameters.Add(new SqlParameter("@shipname", SqlDbType.NVarChar) { Value = createOrderRequestDto.shipname });
                parameters.Add(new SqlParameter("@shipaddress", SqlDbType.NVarChar) { Value = createOrderRequestDto.shipaddress });
                parameters.Add(new SqlParameter("@shipcity", SqlDbType.NVarChar) { Value = createOrderRequestDto.shipcity });
                parameters.Add(new SqlParameter("@shipcountry", SqlDbType.NVarChar) { Value = createOrderRequestDto.shipcountry });
                parameters.Add(new SqlParameter("@orderdate", SqlDbType.DateTime) { Value = createOrderRequestDto.orderdate });
                parameters.Add(new SqlParameter("@requireddate", SqlDbType.DateTime) { Value = createOrderRequestDto.requireddate });
                parameters.Add(new SqlParameter("@shippeddate", SqlDbType.DateTime) { Value = createOrderRequestDto.shippeddate });
                parameters.Add(new SqlParameter("@freight", SqlDbType.Money) { Value = createOrderRequestDto.freight });

                SqlParameter[] Parameterarray = parameters.ToArray();

                using (var rdr = await ExecuteSpResults(_spCreateOrder, Parameterarray))
                {

                    for (int i = 0; i < rdr.Tables[0].Rows.Count; i++)
                    {

                        createOrderResponseDto.Order = Convert.ToInt32(rdr.Tables[0].Rows[i]["orderid"]);

                    }
                    createOrderResponseDto.Success = createOrderResponseDto.Order != 0 ? true : false;
                }

                return createOrderResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
