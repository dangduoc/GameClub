using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models;
public class GameClubEventDto:AuditableEntityDto
{
    public GameClubEventDto(GameClubEvent domain):base(domain)
    {
        this.Id = domain.Id;
        this.Title = domain.Title;
        this.Description = domain.Description;
        this.ScheduledAt = domain.ScheduledAt;
        this.ClubId = domain.ClubId;
        if(domain.Club != null)
        {
            this.Club=new GameClubDto(domain.Club);
        }
    }

    public string Id { get;  set; }
    public string Title { get;  set; }
    public string? Description { get;  set; }
    public DateTime ScheduledAt { get;  set; }
    public string ClubId { get;  set; }
    public GameClubDto? Club { get;  set; }
}