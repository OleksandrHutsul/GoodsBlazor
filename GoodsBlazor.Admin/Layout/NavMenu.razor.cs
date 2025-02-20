using GoodsBlazor.Common.Components.Models;

namespace GoodsBlazor.Admin.Layout;

public partial class NavMenu
{
    private List<NavMenuItem> AdminMenuItems = new()
    {
        new() { Text = "Goods", Url = "/products", IconClass = "fa-solid fa-house" },
        new() { Text = "Create Product", Url = "/create-product", IconClass = "bi bi-box-seam" },
        new() { Text = "New task", Url = "/new", IconClass = "bi bi-box-seam" },
        new() { Text = "Index", Url = "/index", IconClass = "bi bi-box-seam" }
    };
}
