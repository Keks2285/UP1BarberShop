<?php
header('Content-type: application/json; charset=utf-8');
mb_internal_encoding("UTF-8");
mb_internal_encoding('UTF-8');
mb_http_output('UTF-8');
mb_http_input('UTF-8');
mb_regex_encoding('UTF-8');
    function authorization($connect, $data){
            
        $Emloyers=$connect->prepare("select * from Employe join Post where Post_ID=ID_Post and Email=? and Password=?");
        $Emloyers->execute(array(strval($data["email"]), md5(strval($data["password"]) )));
        $listUser=$Emloyers->fetchAll();

        //тут для клиета тоже надо будет сделать
        //$searchUser=$connect->prepare("select * from `Employee` join Studio where Studio_ID=ID_Studio and ID_Anime=?");
        //$searchUser->execute(array($data));
        //$listUser=$searchUser->fetchAll();
           // echo print_r($listUser); die();
           //md5 decode sdd
        if(count($listUser)==0){
            $responce=[
                "status"=>false,
                "message"=>"user not found!"
            ];
            http_response_code(404);
        }
        else{

          //  print_r($listUser); die();
            $responce=[
                "status"=>true,
                "message"=>"Authorizated!",
                "firstname"=>$listUser[0]["FirstName"],
                "lastname"=>$listUser[0]["LastName"],
                "post_id"=>$listUser[0]["Post_ID"]
            ];
            http_response_code(200);
        }
        echo json_encode($responce);
    }


   

   
 
    

    function recoverPassword($connect, $data){
       try{
        if($data["userType"]="Employe"){
            $selectUser=$connect->prepare("update Employe set `Password` = ? where Email= ? ;");
        }else{
            $selectUser=$connect->prepare("update Client set `Password` = ? where Email= ? ;");
        }
       
        $selectUser->execute(array(
            md5(strval($data["newPassword"])),
          strval($data["email"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"User password was Updated",
            "code"=>200
        ];
       
        echo json_encode($responce);
       } catch(Exception $e){
        $responce=[
            "status"=>false,
            "message"=>"User wasn't Updated",
            "code"=>404
        ];
        echo json_encode($responce);
        die();
       }
    }

    

    