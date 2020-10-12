"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.AppComponent = void 0;
var core_1 = require("@angular/core");
var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'ShopPlatform';
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: 'app-root',
            template: "\n  <div class=\"d-flex flex-column flex-md-row align-items-center p-3 px-md-4 mb-3 bg-white border-bottom shadow-sm\">\n    <a class=\"navbar-brand mr-auto  my-0 mr-md-auto font-weight-normal\" style=\"color: black\" routerLink=\"/\">Mail Manager</a>\n    <nav class=\"my-2 my-md-0 mr-md-3\">\n    </nav>\n    <button class=\"btn btn-primary\">\u0412\u0445\u043E\u0434</button>\n  </div>\n  <main role=\"main\" class=\"container\">\n    <router-outlet></router-outlet>\n  </main>",
            styles: []
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
