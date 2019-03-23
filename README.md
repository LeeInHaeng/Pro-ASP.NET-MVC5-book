# Chap1 ASP.NET MVC의 이해
- 애자일 기법
- ASP.NET MVC의 주요 장점
- 책의 예제 소스 링크

# Chap2 첫 번째 MVC 응용프로그램
- Visual Studio 설치 및 초기 프로젝트
- IIS 설치
- Controller에서 View로 데이터 전달 (ViewBag 사용)
  - View에서 간단한 링크 걸기
- MVC 중 Model 작성
- View에서 폼 작성하기 (BeginForm 사용)
  - Strongly Typed View 생성
  - 폼 내용 안에서 HTML 헬퍼 메소드 중 TextBoxFor 사용
  - 폼 내용 안에서 HTML 헬퍼 메소드 중 DropDownListFor 사용
- MVC 중 Controller 작성 (모델 바인딩)
- Model 클래스에 유효성 검사 추가하기
- NuGet을 이용해 필요 라이브러리 설치
- WebMail 헬퍼 메서드를 통해 자동 전자메일 전송 예제 작성

# Chap3 MVC 패턴
- 도메인 모델
- MVC와 다른 디자인 패턴들
- 의존성 주입 (DI) ---> 중요
- 테스트 주도 개발

# Chap4 필수 언어 기능
- Model 클래스에서 getter/setter 자동 등록
- 객체 이니셜라이저 및 익명 형식 사용
- LINQ 사용하기
- 비동기 처리 (async와 await 키워드)

# Chap5 Razor로 작업하기
- 레이아웃을 이용해 작업하기
  - ViewStart 사용
- Razor 표현식 사용하기
  - Model, ViewBag, switch, if, using, foreach

# Chap6 필수 MVC 도구
- 의존성 주입 사용 (Ninject 사용, 생성자 주입)
  - Dependency Resolver 작성
  - AddBindings 설정
  - Dependency Resolver 등록
  - 클래스의 생성자로 인터페이스를 등록하도록 설정
  - 해당 인터페이스로 메소드 사용
- Unit Test Project를 이용해 단위 테스트 수행
- Mock 객체와 Moq를 사용해 단위 테스트 수행하기

# Chap7 SportsStore : 실무 응용 프로그램
- 프로젝트 분할
- 프로젝트 작성 순서(1) : 도메인 모델 작성
  - 모든 Repository에서 접근할 수 있는 인터페이스 Repository 생성
- 프로젝트 작성 순서(2) : WebUI에서 컨트롤러 작성
  - 인터페이스 Repository로 데이터 접근
- 프로젝트 작성 순서(3) : WebUI에서 View 작성
  - 기본 라우터 설정
- 프로젝트 작성 순서(4) : DB 연동하기
- ORM 사용 (Entity Framework 사용)
  - Spring에서 Hibernate와 비슷
  - 인터페이스 레파지토리를 상속 받아 구현
  - DI로 바인딩
- ORM 사용 (Dapper, 저장 프로시저, 순수 SQL 쿼리)
  - Spring에서 Mybatis와 비슷
  - 인터페이스 레파지토리를 상속 받아 구현
  - DI로 바인딩
- 페이징 처리하기 (PagedList 사용)
- URL 개선하기 (RouteConfig)
- 부분 뷰 생성하기

# Chap8 SportsStore : 네비게이션
- 카테고리 기능 추가 및 URL 개선
  - WebUI 프로젝트에서 RouteConfig를 통해 URL 구성을 개선한다.
  - RouteConfig 순서 중요
  - RouteConfig 는 이후의 챕터에서 자세한 설명
- 카테고리 네비게이션 메뉴 구성 (자식 액션 사용)
  - Html.Action 사용
  - Html.ActionLink 사용
- Form 전송 후 리다이렉션 (Html.BeginForm 사용)

# Chap9 SportsStore : 카트 완성하기
- View에서 반복문을 통해 form 태그 생성
  - ViewData.ModelMetadata.Properties 사용
  - BeginForm과 TextBox를 사용

# Chap10 SportsStore : 모바일
- 반응형 웹 디자인 사용하기 (bootstrap 내에서)
- 컨트롤러가 뷰 선택할 때 도와주기
- 모바일 전용 컨텐츠 만들기 (jquery mobile 사용)
  - Jquery Mobile 공부
  - .Mobile.cshtml 이름의 모바일 전용 뷰 따로 작성

# Chap11 SportsStore : 관리
- CRUD 기능 만들기
  - ActionLink, BeginForm 등으로 View에서 Controller로 데이터 전달
  - Controller의 매개변수로 받아 repository에서 메소드 호출 후 처리
  - TempData를 이용해 View에 정보 전달 후, DB 처리 완료를 알림

# Chap12 SportsStore : 보안과 마무리 작업
- 인증을 위해 폼 인증 방식 사용
  - https://github.com/Apress/pro-asp.net-mvc-5 에서 Additional content 참고
- 이미지 업로드 기능 추가
  - DB는 VARBINARY 타입과 VARCHAR 타입 두 개 사용
  - Url.Action 사용
  - form 태그에서 넘어온 이미지에 대한 정보를 매개 변수로 HttpPostedFileBase 객체로 받는다.
  - 이미지를 얻어올 때 컨트롤러에서 FileContentResult 타입 반환

# Chap13 배포
- Azure를 이용한 배포

# Chap14 MVC 프로젝트 개요
- 파일명 규칙과 디버깅

# Chap15 URL 라우팅
- 가변 길이의 라우트 정의
- 라우트에 제약조건 설정
- 어트리뷰트 라우팅 활성화 및 적용하기
  - MVC5 부터 추가된 기능으로, 라우팅에 대해 RouteConfig 파일에 정의하는 것이 아닌 각 컨트롤러의 액션 메소드에 라우팅을 정의하는 형태

# Chap16 고급 라우팅 기능
- ActionLink 사용법
- Area 구분

# Chap23 URL과 Ajax 헬퍼 메서드
- URL을 렌더하는 간단한 HTML 헬퍼 메소드
- Ajax 를 이용해 부분적으로 뷰 그리기
  - 폼 태그 전송시 뷰에서 table 전체를 다시 그리는 것이 아닌 비동기적 처리를 통한 부분적 업데이트를 위해 부분 뷰를 사용
- Ajax 옵션 설정하기
  - Url
  - LoadingElementId와 LoadingElementDuration
  - Confirm
  - @Ajax.ActionLink
  - Ajax 콜백
