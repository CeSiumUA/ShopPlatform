using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopPlatform.API.Controllers;

namespace ShopPlatform.API
{
    public class ServerResponse<T>
    {
        public ServerError Error { get; set; }
        public T Payload;

        public ServerResponse()
        {

        }

        public ServerResponse(ServerError serverError)
        {
            this.Error = serverError;
        }

        public ServerResponse(T payload)
        {
            this.Payload = payload;
        }

        public ServerResponse(ServerError serverError, T payload)
        {
            this.Error = serverError;
            this.Payload = payload;
        }
    }
}
