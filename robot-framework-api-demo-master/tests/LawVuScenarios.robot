*** Settings ***
Resource    ../resources/environment_variables.robot
Resource    ../resources/keywords/assertions.robot
Resource    ../resources/libraries.robot
Suite Setup    Run Keyword    Cleanup Sessions
Suite Teardown    Run Keyword    Cleanup Sessions

*** Test Cases ***

Get Request should return a response body that contains a list of Lawyers
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /Lawyer  params=skip=0&take=101
    Should Contain  ${response.text}  The Nelson and Murdock law firm
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Post Request should create a new json object for the created new lawyer
    Create Session    my_session    ${HOST1}
    &{auth_dict}=    Create Dictionary    firstName    ${firstName}    lastName    ${lastName}  companyName  ${companyName}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    POST On Session   my_session  /Lawyer  json=${auth_dict}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should use an ID and should return a specific lawyer
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /Lawyer/${id}
    Should Be Equal As Strings  ${expectedFirstNameLawyer}  ${response.json()}[firstName]
    Response Status Code Should Be    ${response}    200

Post Request should create a legal matter
    Create Session    my_session    ${HOST1}
    &{auth_dict}=    Create Dictionary    matterName  ${matterName}    lawyerId    ${lawyerId}  lawyerCompanyName  ${lawyerCompanyName}
    ${headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    POST On Session   my_session  /LegalMatter  json=${auth_dict}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should retrieve a LegalMatter (Individual and pagination)
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /LegalMatter  params=skip=0&take=101
    Should Contain  ${response.text}  Saul Goodman & Associates
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Get Request should retrieve a LegalMatter (Individual and pagination) via ID
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /LegalMatter/${legalMatterId}
    Should Be Equal As Strings  ${expectedMatterName}  ${response.json()}[matterName]
    Response Status Code Should Be    ${response}    200

Patch Request should Assign a lawyer to a legal matter
    Create Session    my_session    ${HOST1}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    PATCH On Session   my_session  /LegalMatter  data={"ids":["${idMatter}"],"lawyerId":"${matterlawyerId}"}    headers=${headers}
    Response Status Code Should Be    ${response}    200
    Delete All Sessions

Patch Request should NOT Assign a lawyer to a legal matter when user uses a non existing LawyerId
    Create Session    my_session    ${HOST1}
    &{headers}=  Create Dictionary  Content-Type=application/json  accept=*/*
    ${response}    PATCH On Session   my_session  /LegalMatter  data={"ids":["${idMatter}"],"lawyerId":"${matterlawyerIdNonExisting}"}    headers=${headers}  expected_status=400
    Delete All Sessions

Get Request should NOT retrieve a LegalMatter (Individual and pagination) via ID
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /LegalMatter/${legalMatterIdNonExisting}  expected_status=404
    Response Status Code Should Be    ${response}    404

Get Request should NOT return a specific lawyer when user uses a non existing Lawyer ID
    Create Session    my_session    ${HOST1}
    ${response}    GET On Session   my_session  /Lawyer/${matterlawyerIdNonExisting}  expected_status=404
    Response Status Code Should Be    ${response}    404

*** Keywords ***
Cleanup Sessions
    Delete All Sessions



