using System;
using System.Threading.Tasks;
using APIRESTASPNET.Models;
using APIRESTASPNET.Services;

namespace APIRESTASPNET.Interface
{
    public interface ILoginBusiness
    {
        Task<TokenVO> ValidateCredentials(UserVO user);
    }
}