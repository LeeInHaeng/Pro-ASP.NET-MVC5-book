# 레이아웃을 이용해 작업하기
- 응용프로그램의 외형을 일관적으로 만들어주는 마크업을 작성하기에 좋다
- Views 폴더 오른쪽 클릭 ---> 추가 ---> 새 항목 ---> 웹, MVC 항목 ---> MVC 5 Layout Page (Razor)
  - 공통적으로 적용되는 CSS들 꾸미기
- _ViewStart.cshtml 파일 생성
  - MVC 프레임워크가 뷰를 렌더할 때 해당 파일을 먼저 찾고, Layout을 적용한다.
```
@{
    Layout = "~/Views/_LayoutPage1.cshtml";
}
```
- 원하는 cshtml에서 해당 Layout 사용
  - _ViewStart를 이용해 모든 cshtml에 Layout이 적용된다.
  - 그렇기 때문에 Layout을 사용하지 않기 위해서는 Layout=null 속성을 지정해야 한다.
```
@model Razor.Models.Product

@{
    ViewBag.Title = "Product Name";
}

Product Name : @Model.Name
```
- 처음 View를 만들 때 Use a layout에 체크한 후 빈칸으로 만들면 디폴트로 ViewStart에 등록된 레이아웃을 사용한다.

# Razor 표현식 사용하기
- 중요한 주의 사항
  - Razor을 이용하면 C# 구문들을 실행할 수도 있다. 그러나 어떠한 방식으로든 Razor를 사용해서 업무 로직을 수행하거나 도메인 모델 개체를 조작해서는 안 된다.
  - 반대로 액션 메소드에서 뷰로 전달할 데이터의 서식을 설정해서도 안 된다.
- @Model 키워드를 이용해 Controller에서 넘어온 객체에 대해 접근 가능
- @ViewBag 키워드를 이용해 Controller에서 넘어온 ViewBag에 대해 접근 가능
- @switch 나 @if 키워드를 이용해 조건에 따라 View를 다르게 그려줄 수 있다.
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
- 배열 및 네임스페이스 사용하기 (@foreach 키워드 사용해서 View 적용)
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

// 네임 스페이스 지정 안할 시 : @model Razor.Models.Product[]

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
