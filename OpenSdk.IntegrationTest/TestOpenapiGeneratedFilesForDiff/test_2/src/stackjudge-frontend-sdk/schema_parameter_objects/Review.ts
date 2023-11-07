import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class Review
{
    public constructor(
        private id: number,
        private groupId: number,
        private visibility: number,
        private rate: number,
        private review: string,
        private createdAt: string,
        private createdBy: string,
    ) {
    }
}
