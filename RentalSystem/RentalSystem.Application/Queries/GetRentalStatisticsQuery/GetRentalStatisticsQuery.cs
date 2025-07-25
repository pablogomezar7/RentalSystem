using MediatR;
using RentalSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalSystem.Application.Queries.GetRentalStatisticsQuery
{
    public record GetRentalStatisticsQuery(DateTime From, DateTime To) : IRequest<RentalStatisticsDto>;
}
