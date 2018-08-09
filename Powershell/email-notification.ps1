$upcoming = Invoke-RestMethod http://pgce-organiser-api.azurewebsites.net/api/calendardates/upcoming
$nextWeek = Invoke-RestMethod http://pgce-organiser-api.azurewebsites.net/api/calendardates/nextWeek

$tomorrow = Invoke-RestMethod http://pgce-organiser-api.azurewebsites.net/api/calendardates/tomorrow

$upcomingBody = ''
$nextWeekBody = ''
$tomorrowBody = ''

foreach ($i in $upcoming) {
   foreach ($e in $i.events) {
        $upcomingBody += '<li>{0:d MMM}: <b>{1}</b> - {2}</li>' -f [datetime]$e.dateTime, $e.name, $e.description
    }
}

foreach ($i in $nextWeek) {
   foreach ($e in $i.events) {
        $nextWeekBody += '<li>{0:d MMM}: <b>{1}</b> - {2}</li>' -f [datetime]$e.dateTime, $e.name, $e.description
    }
}

foreach ($i in $tomorrow) {
   foreach ($e in $i.events) {
        $tomorrowBody += '<li>{0:d MMM}: <b>{1}</b> - {2}</li>' -f [datetime]$e.dateTime, $e.name, $e.description
    }
}

if($tomorrowBody -eq ''){
    $tomorrowBody = 'Nothing due tomorrow'
}

if($nextWeekBody -eq ''){
    $nextWeekBody = 'Nothing this week'
}

if($upcomingBody -eq '') {
    $upcomingBody = 'Nothing due this month'
}


$SmtpServer = 'smtp.live.com'
$SmtpUser = 'si-aspinall@hotmail.co.uk'
$smtpPassword = 'Kelly1211'
$MailtTo = 'kelly_hope@live.co.uk'
$MailFrom = 'PGCE Organiser<si-aspinall@hotmail.co.uk>'
$MailSubject = "PGCE Daily Update"
$Credentials = New-Object System.Management.Automation.PSCredential -ArgumentList $SmtpUser, $($smtpPassword | ConvertTo-SecureString -AsPlainText -Force) 
$body = '<div><u>Tomorrow</u><ul style="list-style: none;">{0}</ul></div><div><u>This Week</u><ul style="list-style: none;">{1}</ul></div><div><u>This Month</u><ul style="list-style: none;">{2}</ul></div>' -f $tomorrowBody, $nextWeekBody, $upcomingBody
Send-MailMessage -To "$MailtTo" -from "$MailFrom" -Subject $MailSubject -SmtpServer $SmtpServer -UseSsl -Credential $Credentials -BodyAsHtml $body