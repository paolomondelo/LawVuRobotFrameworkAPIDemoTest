*** Settings ***
Resource    ../resources/environment_variables.robot
Resource    ../resources/keywords/assertions.robot
Resource    ../resources/libraries.robot
Resource    ../resources/page_objects/Lawyer.robot
Resource    ../resources/page_objects/LegalMatter.robot
Suite Setup    Run Keyword    Cleanup Sessions
Suite Teardown    Run Keyword    Cleanup Sessions

*** Test Cases ***

Get Request should return a response body that contains a list of Lawyers
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${lawyer_url}  params=skip=0&take=101
    Should Contain  ${response.text}  ${TC1_Expected_Result}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Post Request should create a new json object for the created new lawyer
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    &{auth_dict}=    Create Dictionary    firstName    ${firstName}    lastName    ${lastName}  companyName  ${companyName}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    POST On Session   my_session  ${lawyer_url}  json=${auth_dict}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should use an ID and should return a specific lawyer
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${lawyer_url}/${id}
    Should Be Equal As Strings  ${expectedFirstNameLawyer}  ${response.json()}[firstName]
    Response Status Code Should Be    ${response}    200

Post Request should create a legal matter
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    &{auth_dict}=    Create Dictionary    matterName  ${matterName}    lawyerId    ${lawyerId}  lawyerCompanyName  ${lawyerCompanyName}
    ${headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    POST On Session   my_session  /LegalMatter  json=${auth_dict}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should retrieve a LegalMatter (Individual and pagination)
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${legalmatter_url}  params=skip=0&take=101
    Should Contain  ${response.text}  ${TC5_Expected_Result}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should retrieve a LegalMatter (Individual and pagination) via ID
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${legalmatter_url}/${legalMatterId}
    Should Be Equal As Strings  ${expectedMatterName}  ${response.json()}[matterName]
    Response Status Code Should Be    ${response}    200

Patch Request should Assign a lawyer to a legal matter
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    PATCH On Session   my_session  ${legalmatter_url}  data={"ids":["${idMatter}"],"lawyerId":"${matterlawyerId}"}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Patch Request should NOT Assign a lawyer to a legal matter when user uses a non existing LawyerId
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    PATCH On Session   my_session  ${legalmatter_url}  data={"ids":["${idMatter}"],"lawyerId":"${matterlawyerIdNonExisting}"}    headers=${headers}  expected_status=400
    Delete All Sessions

Get Request should NOT retrieve a LegalMatter (Individual and pagination) via ID
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${legalmatter_url}/${legalMatterIdNonExisting}  expected_status=404
    Response Status Code Should Be    ${response}    404

Get Request should NOT return a specific lawyer when user uses a non existing Lawyer ID
    [Tags]   Smoke
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  ${lawyer_url}/${matterlawyerIdNonExisting}  expected_status=404
    Response Status Code Should Be    ${response}    404

*** Keywords ***
Cleanup Sessions
    Delete All Sessions




