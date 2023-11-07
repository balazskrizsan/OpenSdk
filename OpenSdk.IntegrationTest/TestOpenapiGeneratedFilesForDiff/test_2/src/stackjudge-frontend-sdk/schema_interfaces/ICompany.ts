import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import {StdResponse} from "../../common/entities/StdResponse";
import * as valueObject from "./../schema_parameter_objects/index";
import {Observable} from "rxjs";

export const API_URL = "/company";

/**
 * @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
 */
export interface ICompany
{
    get(getCompanyParams: IOpenSdkGetable): Observable<StdResponse<valueObject.CompanySearchServiceResponse>>;
    Post(postCompany: IOpenSdkPostable): StdResponse<string>;
}
