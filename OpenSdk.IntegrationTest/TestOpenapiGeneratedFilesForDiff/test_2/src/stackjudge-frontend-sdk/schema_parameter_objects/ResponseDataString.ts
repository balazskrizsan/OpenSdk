import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class ResponseDataString
{
    public constructor(
        private data: string,
        private success: boolean,
        private errorCode: number,
        private requestId: string,
    ) {
    }
}
