import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class RecursiveGroup
{
    public constructor(
        private id: number,
        private name: string,
        private typeId: number,
        private companyId: number,
        private addressId: number,
        private parentId: number,
        private depth: number,
        private path: string,
    ) {
    }
}
