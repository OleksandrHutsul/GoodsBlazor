﻿using GoodsBlazor.DAL.Entities;

namespace GoodsBlazor.BLL.Services.User.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
}
