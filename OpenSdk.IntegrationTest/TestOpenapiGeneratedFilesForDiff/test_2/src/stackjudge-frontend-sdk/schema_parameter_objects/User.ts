import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class User
{
    public constructor(
        private id: number,
        private isEmailUser: boolean,
        private isFacebookUser: boolean,
        private profilePictureUrl: string,
        private username: string,
        private facebookId: number,
    ) {
    }
}
