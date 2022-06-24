import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {StdResponse} from "../../common/entities/StdResponse";
import {User} from "../schema_parameter_objects/User";
import {Observable} from "rxjs";

export const API_URL = "/account/get-by-review-id/{reviewId}"; 

/**
 * @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
 */
export interface IAccountGetByReviewIdByReviewId
{
    get(getByReviewIdRequest: IOpenSdkGetable): Observable<StdResponse<User>>;
}
