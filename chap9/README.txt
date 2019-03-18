# View에서 반복문을 통해 form 태그 생성
- ShippingDetails 클래스의 요소가 너무 많아 반복적인 마크업 코드를 View에 선언해야 한다.
- 이러한 문제의 해결을 위해 반복문을 이용해 form 태그를 생성한다.
- ViewData.ModelMetadata.Properties 를 통해 뷰의 모델 형식에 대한 정보를 얻어온다.
- BeginForm과 TextBox를 사용한다.
  - 여러 라인의 경우에는 TextArea 사용
```
@model SportsStore.Domain.Entities.ShippingDetails

@using (Html.BeginForm())
{
    foreach(var property in ViewData.ModelMetadata.Properties)
    {
        if(property.PropertyName != "Name" && property.PropertyName != "GiftWrap")
        {
            <div class="form-group">
                <label>@(property.DisplayName ?? property.PropertyName)</label>
                @Html.TextBox(property.PropertyName, null,
                    new { @class = "form-control"})
            </div>
        }
    }

        <input class="btn btn-primary" type="submit" value="Complete order"/>
}

// TextArea example
@Html.TextArea(property.PropertyName, null,
                                new { @class = "form-control", rows = 5})
```
