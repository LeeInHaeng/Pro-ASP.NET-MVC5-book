# 도메인 모델
- 도메인 : 응용프로그램이 반드시 지원해야만 하는 현실 세계의 특정 산업이나 업무에 필요한 엔티티, 작업, 규칙들을 규정함으로써 정의하는 것
- 도메인 모델 : 이러한 도메인을 소프트웨어적으로 표현한 것
  - 도메인 형식으로 알려진 C# 형식 (클래스, 구조체 등)들의 모음
- 도메인 객체 : 데이터의 일부분을 표현하기 위해서 도메인 형식의 인스턴스를 생성하는데 여기서 인스턴스가 도매엔 객체이다.

# MVC와 다른 디자인 패턴들
- 스마트 UI 패턴
  - 컨트롤들을 이용해 버튼 눌림, 키 입력, 마우스 이동 등에 대한 이벤트를 발생시켜서 사용자와의 상호작용을 통보해준다.
  - 개발자는 이 이벤트들에 대응하는 코드를 일련의 이벤트 처리기에 작성한다.
  - 신속하게 결과를 얻을 수 있고, 간단한 프로젝트에 이상적이다.
  - 어떤 관심사의 분리도 없이 사용자 인터페이스 처리 코드와 업무 처리 코드가 모두 한 곳에 뒤섞여 있다.
  - 그렇기 때문에 유지보수와 테스트가 어렵다.
- 모델-뷰 아키텍처
  - 업무 로직을 끄집어내어 별도의 도메인 모델로 분리시킨 것
  - 스마트 UI패턴을 개선할 수는 있지만 UI와 도메인 모델이 너무 긴밀하게 통합되어 있어서 각각 개별적인 단위 테스트를 수행하기 어렵다.
- 3-레이어 아키텍처
  - 업무용 응용프로그램에서 가장 폭넓게 사용되는 패턴
  - UI 구현 방식에 아무런 제약도 없고, 뛰어난 관심사의 분리를 제공
  - MVC 패턴과 매우 유사하다.
  - MVC와의 차이점은 UI 레이어가 클릭 및 이벤트 기반의 GUI 프레임워크와 직접적으로 연결되어 있기 때문에 자동화된 단위 테스트의 수행이 사실상 거의 불가능하다.
  - 또한 UI 부분이 매우 복잡해질 수 있기 때문에 철저하게 테스트할 수 없는 UI 관련 코드가 대량으로 만들어질 수도 있다.
- 모델 뷰 프리젠터 패턴 (MVP)
  - 상태가 존재하는 GUI 플랫폼에 보다 손쉽게 적용할 수 있도록 설계된 MVC의 변형 패턴이다.
  - 프리젠터는 MVC의 컨트롤러와 같은 역할을 수행하며, 상태가 존재하는 뷰와 보다 밀접한 관계를 맺고 사용자의 입력과 동작에 따라 UI 구성요소에 출력되는 값들을 직접 관리한다.
- 모델 뷰 뷰 모델 패턴 (MVVM)
  - 가장 최신의 MVC 변형 패턴
  - 뷰 모델(VM)은 UI에 출력될 데이터들에 대한 속성들과 UI에 의해서 호출될 수 있는 데이터에 대한 작업들을 노출하는 C# 클래스다.

# 의존성 주입 (DI) ---> 중요
- 느슨한 결합을 만들기 위해 인터페이스 사용
  - ex) B 클래스에서 A 클래스에 선언된 메소드를 바탕으로 자기만의 메소드를 구현한다고 가정한다.
  - A, B 클래스의 각각 객체 생성 후 메소드 호출로 접근하는 방식이라면 A 클래스 변경 시 B에서 A를 호출하는 메소드명, 인자값 등이 변경 되어야 한다.
  - 인터페이스를 통해 클래스 간에 직접적인 의존성이 존재하지 않도록 보장할 수 있다.
  - ex) B ---> ABIF(인터페이스) <--- A
  - B 클래스는 기능 구현을 위해 ABIF 메소드들을 이용해서 A의 기능을 사용할 수 있다.
- 위의 구조에서 발생하는 문제점
  - B 클래스에서 ABIF를 통해서 A의 기능을 사용하지만, 이 인터페이스를 구현한 객체를 생성하기 위해서는 어쩔 수 없이 A 클래스의 인스턴스를 직접 생성해야만 한다.
  - 결국 구현 객체를 직접 생성하지 않으면서도 인터페이스를 구현한 객체를 얻을 수 있는 방법이 필요하다.
  - 이러한 문제를 해결하기 위해 나온것이 DI 또는 IoC 로 알려진 기술이다.
```
public class B{
	public void BFunction(){
		ABIF abif = new A();
		abif.AFunction();
	}
}
```
- 의존성 끊고 주입하기
  - B 클래스의 생성자로 해당 인터페이스를 인자로 받는다.
  - 의존성 주입 작업을 수행한다.
  - 즉, 먼저 사용하고자 하는 ABIF의 구현 클래스를 선택하고, 해당 클래스의 객체를 생성한 다음, 다시 그 객체를 A클래스의 생성자에게 인자로 전달해야 한다는 의미이다.
```
public class B{
	private ABIF abif;
	
	public B(ABIF abifParam){
		abif = abifParam;
	}
	
	public void BFunction(){
		abif.AFunction();
	}
}

// 사용
ABIF abif = new A();
b = new B(abif);
```
- 의존 관계는 해결이 되었지만, 인터페이스의 실제 구현 객체를 생성해야 하는 문제점이 생겼다.
- 이러한 문제를 해결하기 위해 나온것이 DI 컨테이너 이다.
- DI 컨테이너에서는 ABIF를 컨테이너에 등록한 다음, 앞으로 ABIF의 구현이 요청되면 A 클래스의 객체가 생성되어야 한다고 지정하는 방식이다.
- 또한 응용프로그램에서 B 객체가 필요할 때마다 DI 컨테이너에게 이 객체의 생성을 요청한다.
- 그러면 DI 컨테이너는 B클래스가 ABIF에 대해 의존성을 선언하고 있으며, 이 인터페이스에 대한 구현으로 A클래스가 설정됐다는 것을 알고 있는 상태이다.
- 그 후 DI 컨테이너는 이러한 정보들을 바탕으로 A클래스의 객체를 생성하고, 이를 B 객체를 생성하기 위한 인자로 사용하며, 결과로 B객체를 응용프로그램에서 사용할 수 있도록 객체를 반환해준다.
- 결과적으로 개발자는 new 키워드를 사용해 직접 객체를 생성할 필요가 없어졌으며, DI 컨테이너에게 필요한 객체를 요청하는 방식이 되었다.

# 테스트 주도 개발
- 단위 테스트 : 개별적인 클래스들의 동작을 응용프로그램의 다른 부분들과는 독립적으로 정의하고 검증하는 방식
  - 작성이 쉽고 수행이 간단하다.
  - 알고리즘이나 업무 로직, 그 밖의 백엔드 기반구조 테스트에 적합하다.
- A/A/A 패턴
  - Arrange : 테스트에 대한 조건들을 설정
  - Act : 테스트를 수행
  - Assert : 반환된 결과가 요구 사항을 만족하는지 검증
- TDD (Test-Driven Development)
  - 응용프로그램에 추가할 새로운 기능이나 메소드를 결정한다.
  - 새로운 기능의 동작을 검증할 테스트부터 먼저 작성하고, 테스트를 실패한다.
  - 새로운 기능을 구현하기 위한 코드를 작성한다.
  - 테스트가 성공할 때까지 테스트 수행과 코드 수정을 반복한다.
- 통합 테스트 : 전체 웹 어플리케이션을 구성하는 다수의 구성요소들이 더불어 수행하는 동작들을 정의하고 검증하는 방식
  - 사용자가 UI와 상호작용하는 방식을 설계할 수 있다.
  - 웹 서버와 데이터베이스를 비롯한 응용프로그램이 사용하는 모든 기술적 계층들을 다룰 수 있다.

