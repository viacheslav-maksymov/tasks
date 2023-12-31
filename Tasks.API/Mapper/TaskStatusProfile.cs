﻿using AutoMapper;
using Tasks.API.Models.Category;
using Tasks.API.Models.Logs;
using Tasks.API.Models.Projects;
using Tasks.API.Models.Roles;
using Tasks.API.Models.Task;
using Tasks.API.Models.TaskStatus;
using Tasks.API.Models.User;
using Tasks.Data.Models;
using Tasks.Log.Models;

namespace Tasks.API.Mapper
{
    public sealed class TaskStatusProfile : Profile
    {
        public TaskStatusProfile()
        {
            this.CreateMap<TaskStatusEntity, TaskStatusDto>(); 
            this.CreateMap<TaskStatusCreateDto, TaskStatusEntity>();

            this.CreateMap<TaskCategoryEntity, TaskCategoryDto>();

            this.CreateMap<TaskEntity, TaskDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            this.CreateMap<TaskCreateDto, TaskEntity>();

            this.CreateMap<ProjectEntity, ProjectDto>();

            this.CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SystemUsers.FirstOrDefault().Email))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));

            this.CreateMap<RoleEntity, RoleDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName));

            this.CreateMap<UserCreateDto, SystemUserEntity>()
                .ForMember(dest => dest.SystemUserId, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<UserCreateDto, UserEntity>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tasks, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserSettings, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore()) 
                .ForMember(dest => dest.Roles, opt => opt.Ignore());


            this.CreateMap<TaskEntity, TaskUpdateLog>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.PriorityId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority != null ? src.Priority.PriorityName : null))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : null))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status != null ? src.Status.StatusName : null))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null));
            this.CreateMap<TaskUpdateLog, TaskUpdateLogDto>();
        }
    }
}
