*** Keywords ***
Response Status Code Should Be
    [Arguments]    ${response}    ${status_code}
    Should Be Equal As Strings    ${response.status_code}    ${status_code}



Response Body Should Be
    [Arguments]    ${response}    ${expected_value}
    Should Be Equal As Strings    ${response.content}    ${expected_value}