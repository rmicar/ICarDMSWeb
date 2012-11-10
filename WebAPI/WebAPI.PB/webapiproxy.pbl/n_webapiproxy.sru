$PBExportHeader$n_webapiproxy.sru
namespace
using ICarDMSWeb.@Interface
end namespace

forward
global type n_webapiproxy from NonVisualObject
end type
end forward

global type n_webapiproxy from NonVisualObject
end type
global n_webapiproxy n_webapiproxy

forward prototypes
public subroutine @execute (ICarDMS.@Interface.IPBProxy fromdotnet)
end prototypes

public subroutine @execute (ICarDMS.@Interface.IPBProxy fromdotnet);fromDotNet.Debug ("Ya estoy en powerbuilder !!!!!");
fromDotNet.Debug ("HTTPMethod=" + fromDotNet.HTTPMethod());
fromDotNet.Debug ("DTOName=" + fromDotNet.DTOName());
return
end subroutine

on n_webapiproxy.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_webapiproxy.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on
