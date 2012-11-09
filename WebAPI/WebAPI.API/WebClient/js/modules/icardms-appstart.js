/*

var ICarDMS = Em.Application.create({
    Config: exports.__icardms_config,
    Apps: exports.ZendeskApps || NullAppMarket
});

ICarDMS.Apps.TicketAppContainer = ICarDMS.Apps.TicketAppContainer || ICarDMS.Apps.AppContainer;

// IE8 doesn't like the delete operator here
exports.__icardms_config = null;

ICarDMS.set('views', Em.Object.create());
setUpRawTemplates();

*/

// ******************************************************************************
//https://github.com/tchak/parisjs-app/blob/master/app/main.js
// ******************************************************************************

Ember.LocalStorage = Ember.Object.extend({
    id: 'ember-default-local-storage',

    data: function (key, value) {
        var id = this.get('id');

        if (value === undefined) {
            return JSON.parse(localStorage.getItem(id));
        } else {
            localStorage.setItem(id, JSON.stringify(value));
            return value;
        }

        // Note how we use `cacheable` to minimise deserialization
    }.property().cacheable()

});

// ******************************************************************************
// ICarDMS: es la aplicación que gestiona todo esto.
// ******************************************************************************
var ICarDMS = Em.Application.create({
   // Config: exports.__icardms_config,
    //Apps: exports.ZendeskApps || NullAppMarket
    ready: function () {
        this._super;      
        var adapterNamespace = window.location.pathname.replace(/^\/|\/$/g, '');   
        adapterNamespace += '/api';
        this.RESTAdapter.set('namespace', adapterNamespace);
        Em.Logger.log('ICarDMS: Application ready !');
        ICarDMS.RootView.append();        
    },
    // create LocalStorage instance
    localStorage: Ember.LocalStorage.create({
        id: 'projects-manager-data'
    })

});




// ******************************************************************************
// ICarDMS.RESTAdapter: Es el "driver REST" que utilizará Ember-data
// ICarDMS.Store: Es el almacen de datos REST. Donde se guardan los datos recibidos mediante AJAX. 
// Y los que se enviarán al backend.
// ******************************************************************************
ICarDMS.RESTAdapter = DS.RESTAdapter.create({
        bulkCommit: false,
        plurals: {'tgcliente': 'tgcliente'},
        namespace: ''  //ICarDMSWeb/api'
});
ICarDMS.Store = DS.Store.create({
    revision: 4,   
    adapter: ICarDMS.RESTAdapter
});

// ******************************************************************************
// ICarDMS.Cliente: Es el MODELO de datos para Cliente. ICarDMS.Store almacenará objetos este tipo de objetos.
// ******************************************************************************
ICarDMS.tgcliente = DS.Model.extend({
    url:        'rgclientes', //Para componer la URI Rest. Por defecto, el nombre del objeto (sin namespace) 
    primaryKey: 'codigo',  //Por defecto, la primarykey es "id". Otros casos, hay que indicarlo
    codigo:     DS.attr('number'),
    nombre:     DS.attr('string', { defaultValue: 'pep' }),
    apellido1:  DS.attr('string', { defaultValue: 'pap' }),
    apellido2:  DS.attr('string', { defaultValue: '*' }),
    direccioneditada: DS.attr('string', { defaultValue: '*' }),
    altafec:    DS.attr('date'),
    
    fullName: function () {
        return this.get('nombre') + ' ' + this.get('apellido1');
    }.property('nombre', 'apellido1'),
    
    didLoad: function () {
       this._super();
       //Em.Logger.log('ICarDMS: Cliente cargado. ' + this.get('fullName') + '. ' + this.get('altafec').toDateString())      
    }
});

ICarDMS.tgcliente.reopenClass({
    url: 'tgcliente'
});

// ******************************************************************************
// ICarDMS.listatgclienteController. Es el controlador de la busqueda de clientes.
// ******************************************************************************
ICarDMS.listatgclienteController = Em.ArrayController.create({
    content: [],
    selected: null, 
    searchApellido: '',
    searchCPostal: '',
    pageSize: 20,
    pageCount: 0,
    pageIndex: 0,

    init: function(){
        this._super()
    },

    doSearch: function() {
        var searchApellido = this.get('searchApellido');
        var searchCPostal  = this.get('searchCPostal');
        var pageIndex = this.get('pageIndex');
        var queryString = {};        
        
        if (searchApellido > '') { queryString.apellidosearch = searchApellido };
        if (searchCPostal > '') { queryString.cpostal = searchCPostal };
        if (pageIndex > 0) { queryString.page = pageIndex };
      
        this.set('content', ICarDMS.Store.find(ICarDMS.tgcliente, queryString));
    
        //Em.Logger.log('ICarDMS: doSearch ("' + searchApellido +'")');      
    }.observes('searchApellido','searchCPostal','pageIndex'),

    goPageNext: function () {
        var pageIndex = this.get('pageIndex');
        pageIndex++;
        this.set('pageIndex', pageIndex);
    },

    goPagePrev: function () {
        var pageIndex = this.get('pageIndex');
        pageIndex--;
        if (pageIndex < 0) { pageIndex = 0 };
        this.set('pageIndex', pageIndex);
    },

    rowCount: function () {
        var count = 0;
        count = this.content.get('length');
        return count
    },

    emptyCriteria: function () {
        //if em.em
        return true
    },

    createCliente: function(){
    },
    
    editCliente: function(){
    }   
});

// ******************************************************************************
// ICarDMS.ClienteController. Es el controlador de la edición de cliente.
// ******************************************************************************
ICarDMS.ClienteController = Em.ArrayController.create({
    content: [],    
    codigo: 0,

    init: function () {
        this._super()
    },

    doFind: function () {
       
        //Em.Logger.log('ICarDMS: doSearch ("' + searchApellido +'")');
        this.set('content', ICarDMS.Store.find(ICarDMS.tgcliente, codigo));
    },   

    doUpdate: function () {
        ICarDMS.Store.commit();
    }
  
});


// ******************************************************************************
// ICarDMS.RootView: Es la vista principal.
// Se puede lanzar inicialmente al tenerla como template en el documento html, o llamandola mediante el metodo append()
// Se vincula al controlador "ICarDMS.listatgclienteController"
// ******************************************************************************
ICarDMS.RootView = Em.ContainerView.create({
    classNames: ['nivel1', 'RootView'],
    controller: ICarDMS.listatgclienteController,  
    childViews: ['searchApellido', 'listClientes'],    

    searchApellido: Ember.TextField.create({
                classNames: ['nivel2', 'search'],
                placeholder: 'Buscar por nombre',
                // Here we use a binding to bind the value of the text field to a property in the controller.
                // The property is updated live when user types in the field. The controller has an observer
                // that reacts to changes and updates the list of persons appropriately.
                valueBinding: 'ICarDMS.listatgclienteController.searchApellido',
                keyUp: function() {
                        this._elementValueDidChange();
                        return false;
                    },
            }),

    listClientes: Ember.CollectionView.create({
        classNames: ['nivel2', 'listatgcliente'],
        contentBinding: 'ICarDMS.listatgclienteController.content',
        selectionBinding: 'ICarDMS.listatgclienteController.selected',
        itemViewClass: Ember.View.extend ({
            template: Ember.Handlebars.compile("<b>{{view.content.codigo}}</b>          {{view.content.fullName}} ")
        }),
        emptyView: Ember.View.extend({
            template: Ember.Handlebars.compile("No se han encontrado clientes!")
        })
    })
});
