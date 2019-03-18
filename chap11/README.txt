# CRUD 기능 만들기
- 뷰에서 ActionLink로 컨트롤러에게 ProductID에 대한 정보를 전달
```
                    <td>@Html.ActionLink(item.Name, "Edit", "Admin", 
                       new { productId = item.ProductID }, null)
                    </td>
```
- Controller에서 View에서 전달해준 데이터를 매개변수로 받아서 조작
  - 해당 ProductID에 맞는 정보 갖고오기 ---> FirstOrDefault 사용
```
// AdminController

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }
```
- 컨트롤러가 넘겨준 데이터를 보여줄 View 꾸미기
  - Html.EditorForModel 사용
  - 컨트롤러에서 넘어온 객체에 대해 label과 input 태그를 자동으로 만들어 준다.
  - input 태그의 id와 name은 해당 객체의 속성 이름을 그대로 따온다.
  - 편리하긴 하지만 보여줄 속성과 안보여줄 속성에 대해 객체에서 따로 정의해야 한다.
  - 또한 사용자가 보기 좋은 View를 위해서는 style을 변경해줄 필요성도 있다.
```
@using (Html.BeginForm())
{
    @Html.EditorForModel()
    <input type="submit" value="Save" />
    @Html.ActionLink("Cancle and return to List","Index")
}
```
- Html.EditorForModel에서 보여주는 데이터 수정
  - HiddenInput(DisplayValue = false) 은 데이터를 보여주지 않음을 의미
  - DataType(DataType.MultilineText) 은 여러 라인을 적을 수 있도록 변경
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
- Controller 에서 매개 변수로 post를 통해 넘어온 데이터를 받고, repository로 넘겨서 처리
  - TempData로 View에 메시지를 전송한다.
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
- View에서 TempData를 이용해 메시지 처리를 한다.
```
        @if(TempData["message"] != null)
        {
            <div class="alert alert-success">@TempData["message"]</div>
        }
```