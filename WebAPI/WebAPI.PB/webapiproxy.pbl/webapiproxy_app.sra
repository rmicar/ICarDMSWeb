$PBExportHeader$webapiproxy_app.sra
forward
global type webapiproxy_app from Application
end type
global Transaction sqlca
global DynamicDescriptionArea sqlda
global DynamicStagingArea sqlsa
global Error error
global Message message
end forward

global type webapiproxy_app from Application
string AppName = "icardmsbo"
end type
global webapiproxy_app webapiproxy_app

on webapiproxy_app.create
sqlca=create Transaction
sqlda=create DynamicDescriptionArea
sqlsa=create DynamicStagingArea
error=create Error
message=create Message
end on

on webapiproxy_app.destroy
destroy(sqlca)
destroy(sqlda)
destroy(sqlsa)
destroy(error)
destroy(message)
end on
