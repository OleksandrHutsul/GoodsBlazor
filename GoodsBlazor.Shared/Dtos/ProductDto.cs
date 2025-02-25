﻿namespace GoodsBlazor.Shared.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; } = default!;
    public string? ImageBase64 { get; set; }
}