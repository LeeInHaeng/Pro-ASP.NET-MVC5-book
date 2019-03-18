# Model 클래스에서 getter/setter 자동 등록
- 직접 구현이 필요한 속성은 getter와 setter을 직접 구현한다
- 그 외에는 get; set; 을 사용해 getter와 setter을 자동으로 등록한다.
```
        private string name;

        public string Name
        {
            get
            {
                return "hihi" + name;
            }
            set
            {
                name = value;
            }
        }

        public string Description
        { get; set; }
```

# 객체 이니셜라이저 및 익명 형식 사용
- 객체가 생성되는 동시에 setter 기능으로 속성 값들이 바로 설정되도록 한다.
- var 키워드를 통해 익명 형식의 객체를 선언한다.
```
// 기존에는 Product myProduct = new Product{ ... };

            var myProduct = new
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
```

# LINQ 사용하기
- 클래스에 존재하는 데이터를 질의하기 위한 SQL과 유사한 구문이다.
- 마침표 표기법 구문을 사용하여 많은 기능을 사용할 수 있다.
- 예를들어 비싼 가격 순으로 상품을 찾는데, 3개의 결과를 얻고 싶은 경우
```
// Dot Notation을 사용하는 경우 ---> SQL 문법과 약간 달라서 구현이 어려울 수 있다.

            Product[] products =
            {
                new Product {Name="Kayak", Category="Watersports", Price=275M },
                new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
                new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                new Product {Name="Corner flag", Category="Soccer", Price=34.95M }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });

            StringBuilder result = new StringBuilder();
            foreach (var fp in foundProducts)
                result.AppendFormat("Price : {0}", fp.Price);

// Dot Notation을 사용하지 않는 경우

            var foundProducts2 = from match in products
                                 orderby match.Price descending
                                 select new { match.Name, match.Price };

	// SQL 질의와 모습이 비슷해 사용하기 편하지만
	// 3개의 결과를 얻기 위해서는 foreach에서 count를 이용해 3개가 되는 경우
	// break 하도록 해야하는 불편함이 있다.
```
- 간단한 Join 볼만한 링크
  - https://stackoverflow.com/questions/1511833/linq-dot-notation-equivalent-for-join/1511857

# 비동기 처리 (async와 await 키워드)
- async 키워드를 메소드에 사용하며, 비동기 처리 작업을 기다릴 부분에 await 키워드를 사용한다.
```
        public async static Task<long?> GetPageLength()
        {
            HttpClient clinet = new HttpClient();

            var httpMessage = await clinet.GetAsync("http://apress.com");

            // HTTP 요청이 완료되기를 기다리는 동안
            // 여기서 다른 작업을 처리할 수 있다.

            return httpMessage.Content.Headers.ContentLength;
        }
```
