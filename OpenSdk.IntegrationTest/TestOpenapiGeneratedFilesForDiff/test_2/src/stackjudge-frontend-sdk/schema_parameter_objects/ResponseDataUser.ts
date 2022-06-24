import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import {User} from "./User";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class ResponseDataUser
{
    public constructor(
        private user: User,
        private success: boolean,
        private errorCode: number,
        private requestId: string,
    ) {
    }
}
