# Chap1 ASP.NET MVC�� ����
- ������ ���
- ASP.NET MVC�� �ֿ� ����
- å�� ���� �ҽ� ��ũ

# Chap2 ù ��° MVC �������α׷�
- Visual Studio ��ġ �� �ʱ� ������Ʈ
- IIS ��ġ
- Controller���� View�� ������ ���� (ViewBag ���)
  - View���� ������ ��ũ �ɱ�
- MVC �� Model �ۼ�
- View���� �� �ۼ��ϱ� (BeginForm ���)
  - Strongly Typed View ����
  - �� ���� �ȿ��� HTML ���� �޼ҵ� �� TextBoxFor ���
  - �� ���� �ȿ��� HTML ���� �޼ҵ� �� DropDownListFor ���
- MVC �� Controller �ۼ� (�� ���ε�)
- Model Ŭ������ ��ȿ�� �˻� �߰��ϱ�
- NuGet�� �̿��� �ʿ� ���̺귯�� ��ġ
- WebMail ���� �޼��带 ���� �ڵ� ���ڸ��� ���� ���� �ۼ�

# Chap3 MVC ����
- ������ ��
- MVC�� �ٸ� ������ ���ϵ�
- ������ ���� (DI) ---> �߿�
- �׽�Ʈ �ֵ� ����

# Chap4 �ʼ� ��� ���
- Model Ŭ�������� getter/setter �ڵ� ���
- ��ü �̴ϼȶ����� �� �͸� ���� ���
- LINQ ����ϱ�
- �񵿱� ó�� (async�� await Ű����)

# Chap5 Razor�� �۾��ϱ�
- ���̾ƿ��� �̿��� �۾��ϱ�
  - ViewStart ���
- Razor ǥ���� ����ϱ�
  - Model, ViewBag, switch, if, using, foreach

# Chap6 �ʼ� MVC ����
- ������ ���� ��� (Ninject ���, ������ ����)
  - Dependency Resolver �ۼ�
  - AddBindings ����
  - Dependency Resolver ���
  - Ŭ������ �����ڷ� �������̽��� ����ϵ��� ����
  - �ش� �������̽��� �޼ҵ� ���
- Unit Test Project�� �̿��� ���� �׽�Ʈ ����
- Mock ��ü�� Moq�� ����� ���� �׽�Ʈ �����ϱ�

# Chap7 SportsStore : �ǹ� ���� ���α׷�
- ������Ʈ ����
- ������Ʈ �ۼ� ����(1) : ������ �� �ۼ�
  - ��� Repository���� ������ �� �ִ� �������̽� Repository ����
- ������Ʈ �ۼ� ����(2) : WebUI���� ��Ʈ�ѷ� �ۼ�
  - �������̽� Repository�� ������ ����
- ������Ʈ �ۼ� ����(3) : WebUI���� View �ۼ�
  - �⺻ ����� ����
- ������Ʈ �ۼ� ����(4) : DB �����ϱ�
- ORM ��� (Entity Framework ���)
  - Spring���� Hibernate�� ���
  - �������̽� �������丮�� ��� �޾� ����
  - DI�� ���ε�
- ORM ��� (Dapper, ���� ���ν���, ���� SQL ����)
  - Spring���� Mybatis�� ���
  - �������̽� �������丮�� ��� �޾� ����
  - DI�� ���ε�
- ����¡ ó���ϱ� (PagedList ���)
- URL �����ϱ� (RouteConfig)
- �κ� �� �����ϱ�

# Chap8 SportsStore : �׺���̼�
- ī�װ� ��� �߰� �� URL ����
  - WebUI ������Ʈ���� RouteConfig�� ���� URL ������ �����Ѵ�.
  - RouteConfig ���� �߿�
  - RouteConfig �� ������ é�Ϳ��� �ڼ��� ����
- ī�װ� �׺���̼� �޴� ���� (�ڽ� �׼� ���)
  - Html.Action ���
  - Html.ActionLink ���
- Form ���� �� �����̷��� (Html.BeginForm ���)

# Chap9 SportsStore : īƮ �ϼ��ϱ�
- View���� �ݺ����� ���� form �±� ����
  - ViewData.ModelMetadata.Properties ���
  - BeginForm�� TextBox�� ���

# Chap10 SportsStore : �����
- ������ �� ������ ����ϱ� (bootstrap ������)
- ��Ʈ�ѷ��� �� ������ �� �����ֱ�
- ����� ���� ������ ����� (jquery mobile ���)
  - Jquery Mobile ����
  - .Mobile.cshtml �̸��� ����� ���� �� ���� �ۼ�

# Chap11 SportsStore : ����
- CRUD ��� �����
  - ActionLink, BeginForm ������ View���� Controller�� ������ ����
  - Controller�� �Ű������� �޾� repository���� �޼ҵ� ȣ�� �� ó��
  - TempData�� �̿��� View�� ���� ���� ��, DB ó�� �ϷḦ �˸�

# Chap12 SportsStore : ���Ȱ� ������ �۾�
- ������ ���� �� ���� ��� ���
  - https://github.com/Apress/pro-asp.net-mvc-5 ���� Additional content ����
- �̹��� ���ε� ��� �߰�
  - DB�� VARBINARY Ÿ�԰� VARCHAR Ÿ�� �� �� ���
  - Url.Action ���
  - form �±׿��� �Ѿ�� �̹����� ���� ������ �Ű� ������ HttpPostedFileBase ��ü�� �޴´�.
  - �̹����� ���� �� ��Ʈ�ѷ����� FileContentResult Ÿ�� ��ȯ

# Chap13 ����
- Azure�� �̿��� ����

# Chap14 MVC ������Ʈ ����
- ���ϸ� ��Ģ�� �����

# Chap15 URL �����
- ���� ������ ���Ʈ ����
- ���Ʈ�� �������� ����
- ��Ʈ����Ʈ ����� Ȱ��ȭ �� �����ϱ�
  - MVC5 ���� �߰��� �������, ����ÿ� ���� RouteConfig ���Ͽ� �����ϴ� ���� �ƴ� �� ��Ʈ�ѷ��� �׼� �޼ҵ忡 ������� �����ϴ� ����

# Chap16 ��� ����� ���
- ActionLink ����
- Area ����

# Chap23 URL�� Ajax ���� �޼���
- URL�� �����ϴ� ������ HTML ���� �޼ҵ�
- Ajax �� �̿��� �κ������� �� �׸���
  - �� �±� ���۽� �信�� table ��ü�� �ٽ� �׸��� ���� �ƴ� �񵿱��� ó���� ���� �κ��� ������Ʈ�� ���� �κ� �並 ���
- Ajax �ɼ� �����ϱ�
  - Url
  - LoadingElementId�� LoadingElementDuration
  - Confirm
  - @Ajax.ActionLink
  - Ajax �ݹ�
