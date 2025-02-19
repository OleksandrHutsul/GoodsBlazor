namespace GoodsBlazor.Shared.Dtos;

public class ProductTypeDto
{
    public int Id { get; set; }
    public string TypeName { get; set; } = default!;

    public override string ToString()
    {
        return TypeName;
    }
}
