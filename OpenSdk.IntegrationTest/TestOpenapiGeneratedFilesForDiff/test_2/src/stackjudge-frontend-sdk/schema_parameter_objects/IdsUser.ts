import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class IdsUser
{
    public constructor(
        private sub: string,
        private userName: string,
        private normalizedUserName: string,
        private email: string,
        private emailConfirmed: boolean,
        private profileUrl: string,
    ) {
    }
}
