# CRUD ��� �����
- �信�� ActionLink�� ��Ʈ�ѷ����� ProductID�� ���� ������ ����
```
                    <td>@Html.ActionLink(item.Name, "Edit", "Admin", 
                       new { productId = item.ProductID }, null)
                    </td>
```
- Controller���� View���� �������� �����͸� �Ű������� �޾Ƽ� ����
  - �ش� ProductID�� �´� ���� ������� ---> FirstOrDefault ���
```
// AdminController

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }
```
- ��Ʈ�ѷ��� �Ѱ��� �����͸� ������ View �ٹ̱�
  - Html.EditorForModel ���
  - ��Ʈ�ѷ����� �Ѿ�� ��ü�� ���� label�� input �±׸� �ڵ����� ����� �ش�.
  - input �±��� id�� name�� �ش� ��ü�� �Ӽ� �̸��� �״�� ���´�.
  - ���ϱ� ������ ������ �Ӽ��� �Ⱥ����� �Ӽ��� ���� ��ü���� ���� �����ؾ� �Ѵ�.
  - ���� ����ڰ� ���� ���� View�� ���ؼ��� style�� �������� �ʿ伺�� �ִ�.
```
@using (Html.BeginForm())
{
    @Html.EditorForModel()
    <input type="submit" value="Save" />
    @Html.ActionLink("Cancle and return to List","Index")
}
```
- Html.EditorForModel���� �����ִ� ������ ����
  - HiddenInput(DisplayValue = false) �� �����͸� �������� ������ �ǹ�
  - DataType(DataType.MultilineText) �� ���� ������ ���� �� �ֵ��� ����
```
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

	...

	[DataType(DataType.MultilineText)]
        public string Description { get; set; }

	...
    }
```
- Controller ���� �Ű� ������ post�� ���� �Ѿ�� �����͸� �ް�, repository�� �Ѱܼ� ó��
  - TempData�� View�� �޽����� �����Ѵ�.
```
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
```
- View���� TempData�� �̿��� �޽��� ó���� �Ѵ�.
```
        @if(TempData["message"] != null)
        {
            <div class="alert alert-success">@TempData["message"]</div>
        }
```