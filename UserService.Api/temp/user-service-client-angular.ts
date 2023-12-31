//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class ProfileClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    get(): Observable<ApiResultOfUserForDetails> {
        let url_ = this.baseUrl + "/api/Profile";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGet(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGet(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfUserForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfUserForDetails>;
        }));
    }

    protected processGet(response: HttpResponseBase): Observable<ApiResultOfUserForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfUserForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfUserForDetails>(null as any);
    }
}

@Injectable()
export class RolesAdminClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getRoles(page: number | undefined, take: number | undefined, skip: number | undefined): Observable<ApiResultOfGetPaginationResultOfRoleForList> {
        let url_ = this.baseUrl + "/api/Admin/Roles?";
        if (page === null)
            throw new Error("The parameter 'page' cannot be null.");
        else if (page !== undefined)
            url_ += "Page=" + encodeURIComponent("" + page) + "&";
        if (take === null)
            throw new Error("The parameter 'take' cannot be null.");
        else if (take !== undefined)
            url_ += "Take=" + encodeURIComponent("" + take) + "&";
        if (skip === null)
            throw new Error("The parameter 'skip' cannot be null.");
        else if (skip !== undefined)
            url_ += "Skip=" + encodeURIComponent("" + skip) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetRoles(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetRoles(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfGetPaginationResultOfRoleForList>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfGetPaginationResultOfRoleForList>;
        }));
    }

    protected processGetRoles(response: HttpResponseBase): Observable<ApiResultOfGetPaginationResultOfRoleForList> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfGetPaginationResultOfRoleForList;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfGetPaginationResultOfRoleForList>(null as any);
    }

    postRole(role: RoleForCreating): Observable<ApiResultOfRoleForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Roles";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(role);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPostRole(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPostRole(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfRoleForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfRoleForDetails>;
        }));
    }

    protected processPostRole(response: HttpResponseBase): Observable<ApiResultOfRoleForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfRoleForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfRoleForDetails>(null as any);
    }

    getSelectOptions(): Observable<ApiResultOfIEnumerableOfSelectOption> {
        let url_ = this.baseUrl + "/api/Admin/Roles/SelectOptions";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetSelectOptions(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetSelectOptions(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfIEnumerableOfSelectOption>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfIEnumerableOfSelectOption>;
        }));
    }

    protected processGetSelectOptions(response: HttpResponseBase): Observable<ApiResultOfIEnumerableOfSelectOption> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfIEnumerableOfSelectOption;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfIEnumerableOfSelectOption>(null as any);
    }

    getRole(id: string): Observable<ApiResultOfRoleForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Roles/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetRole(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetRole(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfRoleForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfRoleForDetails>;
        }));
    }

    protected processGetRole(response: HttpResponseBase): Observable<ApiResultOfRoleForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfRoleForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfRoleForDetails>(null as any);
    }

    putRole(id: string, role: RoleForEditing): Observable<ApiResultOfRoleForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Roles/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(role);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPutRole(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPutRole(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfRoleForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfRoleForDetails>;
        }));
    }

    protected processPutRole(response: HttpResponseBase): Observable<ApiResultOfRoleForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfRoleForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfRoleForDetails>(null as any);
    }

    deleteRole(id: string): Observable<ApiResult> {
        let url_ = this.baseUrl + "/api/Admin/Roles/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDeleteRole(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDeleteRole(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResult>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResult>;
        }));
    }

    protected processDeleteRole(response: HttpResponseBase): Observable<ApiResult> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResult;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResult>(null as any);
    }
}

@Injectable()
export class UsersAdminClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getUsers(page: number | undefined, take: number | undefined, skip: number | undefined): Observable<ApiResultOfGetPaginationResultOfUserForList> {
        let url_ = this.baseUrl + "/api/Admin/Users?";
        if (page === null)
            throw new Error("The parameter 'page' cannot be null.");
        else if (page !== undefined)
            url_ += "Page=" + encodeURIComponent("" + page) + "&";
        if (take === null)
            throw new Error("The parameter 'take' cannot be null.");
        else if (take !== undefined)
            url_ += "Take=" + encodeURIComponent("" + take) + "&";
        if (skip === null)
            throw new Error("The parameter 'skip' cannot be null.");
        else if (skip !== undefined)
            url_ += "Skip=" + encodeURIComponent("" + skip) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetUsers(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetUsers(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfGetPaginationResultOfUserForList>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfGetPaginationResultOfUserForList>;
        }));
    }

    protected processGetUsers(response: HttpResponseBase): Observable<ApiResultOfGetPaginationResultOfUserForList> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfGetPaginationResultOfUserForList;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfGetPaginationResultOfUserForList>(null as any);
    }

    postUser(user: UserForCreating): Observable<ApiResultOfUserForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Users";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(user);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPostUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPostUser(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfUserForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfUserForDetails>;
        }));
    }

    protected processPostUser(response: HttpResponseBase): Observable<ApiResultOfUserForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfUserForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfUserForDetails>(null as any);
    }

    getUser(id: string): Observable<ApiResultOfUserForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Users/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetUser(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfUserForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfUserForDetails>;
        }));
    }

    protected processGetUser(response: HttpResponseBase): Observable<ApiResultOfUserForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfUserForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfUserForDetails>(null as any);
    }

    putUser(id: string, user: UserForEditing): Observable<ApiResultOfUserForDetails> {
        let url_ = this.baseUrl + "/api/Admin/Users/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(user);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPutUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPutUser(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResultOfUserForDetails>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResultOfUserForDetails>;
        }));
    }

    protected processPutUser(response: HttpResponseBase): Observable<ApiResultOfUserForDetails> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResultOfUserForDetails;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResultOfUserForDetails>(null as any);
    }

    deleteUser(id: string): Observable<ApiResult> {
        let url_ = this.baseUrl + "/api/Admin/Users/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDeleteUser(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDeleteUser(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<ApiResult>;
                }
            } else
                return _observableThrow(response_) as any as Observable<ApiResult>;
        }));
    }

    protected processDeleteUser(response: HttpResponseBase): Observable<ApiResult> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver) as ApiResult;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<ApiResult>(null as any);
    }
}

export interface ApiResult {
    code: number;
    errors?: string[] | undefined;
}

export interface ApiResultOfUserForDetails extends ApiResult {
    data?: UserForDetails | undefined;
}

export interface UserForDetails {
    userId: string;
    userName?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;
    roleIds?: string[] | undefined;
}

export interface ApiResultOfGetPaginationResultOfRoleForList extends ApiResult {
    data?: GetPaginationResultOfRoleForList | undefined;
}

export interface GetPaginationResultOfRoleForList {
    data: RoleForList[];
    page: number;
    take: number;
    totalRecord: number;
    totalPage: number;
    firstPage: number;
    lastPage: number;
}

export interface RoleForList {
    roleId: string;
    name?: string | undefined;
}

export interface ApiResultOfIEnumerableOfSelectOption extends ApiResult {
    data?: SelectOption[] | undefined;
}

export interface SelectOption {
    value?: string | undefined;
    title?: string | undefined;
}

export interface ApiResultOfRoleForDetails extends ApiResult {
    data?: RoleForDetails | undefined;
}

export interface RoleForDetails {
    roleId: string;
    name?: string | undefined;
}

export interface RoleForCreating {
    name?: string | undefined;
}

export interface RoleForEditing {
    roleId: string;
    name?: string | undefined;
}

export interface ApiResultOfGetPaginationResultOfUserForList extends ApiResult {
    data?: GetPaginationResultOfUserForList | undefined;
}

export interface GetPaginationResultOfUserForList {
    data: UserForList[];
    page: number;
    take: number;
    totalRecord: number;
    totalPage: number;
    firstPage: number;
    lastPage: number;
}

export interface UserForList {
    userId: string;
    userName?: string | undefined;
}

export interface UserForCreating {
    userName?: string | undefined;
    password?: string | undefined;
    roleIds?: string[] | undefined;
}

export interface UserForEditing {
    userId: string;
    userName?: string | undefined;
    password?: string | undefined;
    roleIds?: string[] | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}