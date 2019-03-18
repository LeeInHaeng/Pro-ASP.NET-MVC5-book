# ���̾ƿ��� �̿��� �۾��ϱ�
- �������α׷��� ������ �ϰ������� ������ִ� ��ũ���� �ۼ��ϱ⿡ ����
- Views ���� ������ Ŭ�� ---> �߰� ---> �� �׸� ---> ��, MVC �׸� ---> MVC 5 Layout Page (Razor)
  - ���������� ����Ǵ� CSS�� �ٹ̱�
- _ViewStart.cshtml ���� ����
  - MVC �����ӿ�ũ�� �並 ������ �� �ش� ������ ���� ã��, Layout�� �����Ѵ�.
```
@{
    Layout = "~/Views/_LayoutPage1.cshtml";
}
```
- ���ϴ� cshtml���� �ش� Layout ���
  - _ViewStart�� �̿��� ��� cshtml�� Layout�� ����ȴ�.
  - �׷��� ������ Layout�� ������� �ʱ� ���ؼ��� Layout=null �Ӽ��� �����ؾ� �Ѵ�.
```
@model Razor.Models.Product

@{
    ViewBag.Title = "Product Name";
}

Product Name : @Model.Name
```
- ó�� View�� ���� �� Use a layout�� üũ�� �� ��ĭ���� ����� ����Ʈ�� ViewStart�� ��ϵ� ���̾ƿ��� ����Ѵ�.

# Razor ǥ���� ����ϱ�
- �߿��� ���� ����
  - Razor�� �̿��ϸ� C# �������� ������ ���� �ִ�. �׷��� ��� ������ε� Razor�� ����ؼ� ���� ������ �����ϰų� ������ �� ��ü�� �����ؼ��� �� �ȴ�.
  - �ݴ�� �׼� �޼ҵ忡�� ��� ������ �������� ������ �����ؼ��� �� �ȴ�.
- @Model Ű���带 �̿��� Controller���� �Ѿ�� ��ü�� ���� ���� ����
- @ViewBag Ű���带 �̿��� Controller���� �Ѿ�� ViewBag�� ���� ���� ����
- @switch �� @if Ű���带 �̿��� ���ǿ� ���� View�� �ٸ��� �׷��� �� �ִ�.
```
            <td>
                @switch ((int)ViewBag.ProductCount)
                {
                    case 0:
                        @: Out of Stock
                        break;
                    default:
                        @ViewBag.ProductCount
                        break;
                }
            </td>
```
```
                @if (ViewBag.ProductCount == 0)
                {
                    @: Out of Stock
                }
                else if(ViewBag.ProductCount == 1)
                {
                    <b>Low Stock (@ViewBag.ProductCount)</b>
                }
                else
                {
                    @ViewBag.ProductCount
                }
```
- �迭 �� ���ӽ����̽� ����ϱ� (@foreach Ű���� ����ؼ� View ����)
```
// Controller

        public ActionResult DemoArray()
        {
            Product[] array =
            {
                new Product {Name="Kayak", Price= 275M },
                new Product {Name = "Lifejacket", Price = 48.95M },
                new Product {Name = "Soccer ball", Price = 19.50M },
                new Product {Name = "Corner flag", Price = 34.95M }
            };

            return View(array);
        }

// View

@using Razor.Models
@model Product[]

// ���� �����̽� ���� ���� �� : @model Razor.Models.Product[]

@if (Model.Length > 0)
{
        <tbody>
            @foreach(var p in Model)
            {
                <tr>
                    <td>@p.Name</td>
                    <td>@p.Price</td>
                </tr>
            }
        </tbody>
}
```
