# View���� �ݺ����� ���� form �±� ����
- ShippingDetails Ŭ������ ��Ұ� �ʹ� ���� �ݺ����� ��ũ�� �ڵ带 View�� �����ؾ� �Ѵ�.
- �̷��� ������ �ذ��� ���� �ݺ����� �̿��� form �±׸� �����Ѵ�.
- ViewData.ModelMetadata.Properties �� ���� ���� �� ���Ŀ� ���� ������ ���´�.
- BeginForm�� TextBox�� ����Ѵ�.
  - ���� ������ ��쿡�� TextArea ���
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
