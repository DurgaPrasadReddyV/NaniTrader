﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NaniTrader.Fyers
{
    public interface IFyersCredentialsAppService : IApplicationService
    {
        Task<FyersCredentialsDto> CreateAsync(CreateFyersCredentialsDto input);
        Task UpdateAsync(Guid id, UpdateFyersCredentialsDto input);
        Task DeleteAsync(Guid id);
        Task GenerateTokenAsync(string fyersApp, string authCode);
        Task<FyersCredentialsDto> GetAsync(Guid id);
        Task<PagedResultDto<FyersCredentialsDto>> GetListAsync(GetFyersCredentialsListDto input);
    }
}
