
/*
(function() {

    // ## ICarDMS.RootView
    // The view representing the whole page. Useful for trapping any global
    // events as the bubble up from contained views.
    ICarDMS.RootView = Em.View.extend({

        init: function() {
            var ret = this._super();
            Lotus.GlobalEvents.set('firstResponder', this);
            return ret;
        }

});



(function () {

    ICarDMS.SearchFieldView = Em.TextField.extend(
      Lotus.Mixins.ResponderView,
      {

          cancel: function () {
              ICarDMS.searchController.cancel();
          },

          isNumRe: /^\d+$/,

          _updateElementValue: function () {
              this._super.apply(this, arguments);
              this.$().focus();
          }.observes('value'),

          insertNewline: function () {
              var value = $.trim(this.get('value'));

              if (this.isNumRe.test(value)) {
                  window.location = '#/tickets/' + value;
              } else {
                  ICarDMS.searchController.set('query', this.get('value'));
              }
          },

          initialFocus: function () {
              if (ICarDMS.get('section') === 'search') {
                  var focusCursor = $.proxy(function () { this.$().focus().select(); }, this);
                  setTimeout(focusCursor, 100);
              }
          }.observes('ICarDMS.section'),

          setFocus: function () {
              if (ICarDMS.searchController.get('navigateSearchResults')) {
                  return;
              }

              this.focus();
              this.$().focus();
              // force text field cursor to end of text
              setTimeout($.proxy(function () { this.$().val(ICarDMS.searchController.get('query')); }, this), 10);
          }.observes('ICarDMS.searchController.navigateSearchResults'),

          navigateToResults: function () {
              if (!ICarDMS.searchController.get('searchResults')) { return; }

              this.$().blur();
              ICarDMS.searchController.set('navigateSearchResults', true);
          },

          interpretKeyEvents: function (e) {
              // 40: down arrow
              e.keyCode === 40 ? this.navigateToResults() : this._super.apply(this, arguments);
          }
      }
    );

}());


*/