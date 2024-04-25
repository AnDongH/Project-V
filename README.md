# Project-V
한양대학교 ERICA 캠퍼스 가상 및 증강 현실 프로그래밍 팀 프로젝트
- 2018037356 안동현
- 백윤성
- 신수연
- 최선아

# 사용 API
XR Interaction ToolKit (Mac 지원)

# Unity Version
2022.3.24f1 LTS

# Game Flow Chart
![게임 흐름 drawio](https://github.com/AnDongH/Project-V/assets/87707867/24549a0c-5e71-41d4-91f0-11302e38550f)

# App Functional Flow Chart
![앱 구상도 drawio](https://github.com/AnDongH/Project-V/assets/87707867/c07dfcb5-1e1e-455d-99af-0e79d33403b5)

# Commit Convention
- feat: 새로운 기능을 추가
- fix: 버그 수정
- design: CSS 등 사용자 UI 디자인 변경
- style: 코드 포맷 변경, 세미 콜론 누락, 코드 수정이 없는 경우
- refactor:	프로덕션 코드 리팩토링
- comment: 필요한 주석 추가 및 변경
- docs:	문서 수정
- test:	테스트 코드, 리펙토링 테스트 코드 추가, Production Code(실제로 사용하는 코드) 변경 없음
- chore:	빌드 업무 수정, 패키지 매니저 수정, 패키지 관리자 구성 등 업데이트, Production Code 변경 없음

위 키워드와 함께 커밋에 관한 간단한 얘기를 아래와 영어로 같이 작성
- 예) feat: update ani model

# Branch 전략
feature 단위로 분기를 만들고 수시로 main 브랜치에 PR을 통해 머지후 브랜치 제거
- 에) feature/ani-model -> main merge
