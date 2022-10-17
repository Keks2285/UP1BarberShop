<?php
header('Content-type: application/json; charset=utf-8');
mb_internal_encoding("UTF-8");
mb_internal_encoding('UTF-8');
mb_http_output('UTF-8');
mb_http_input('UTF-8');
mb_regex_encoding('UTF-8');
    function authorization($connect, $data){
            
        $searchUser=$connect->prepare("select * from Employe join Post where Post_ID=ID_Post and Email=? and Password=?");
        $searchUser->execute(array(strval($data["email"]), md5(strval($data["password"]) )));
        $listUser=$searchUser->fetchAll();

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


    function getposts($connect, $data){
        try{
        $searchUser=$connect->prepare("SELECT ID_Post, Name_Post, Price from `Post`;");
        $searchUser->execute();
        $listUser=$searchUser->fetchAll();

        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }

    }

    function getemployers($connect, $data){
        try{
        $searchUser=$connect->prepare("SELECT ID_Employee, ID_Status, ID_Post, FirstName, LastName, MiddleName, Email, INN, Name_Post, Name_Status FROM `Employe` join Post join Status_Employee where Post_ID=ID_Post and Status_ID=ID_Status;");
        $searchUser->execute();
        $listUser=$searchUser->fetchAll();

        echo json_encode($listUser);
     } catch (PDOException $e){
        http_response_code(404);
     }

    }

    function createemployee ($connect, $data){


       // print_r( $data); die();
        try{
            $selectUsers=$connect->prepare("Select * from Employe where Email=?");
            $selectUsers->execute(array(strval($data["email"])));
            if(count($selectUsers->fetchAll())>0){
                $responce=[
                    "status"=>false,
                    "message"=>"user not created"
                ];
                echo json_encode($responce);
                die();
            }



            $createEmploye=$connect->prepare(
            "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES 
            (?,?,?,?,?,?,?,?)"
            );
            
            //if(empty($data["middlename"])) $data["middlename"]="-";
            $createEmploye->execute(array(
                strval($data["firstname"]), 
                strval($data["lastname"]), 
                strval($data["middlename"]), 
                strval($data["email"]),  
                md5(strval($data["password"])), 
                strval($data["inn"]),
                $data["post_id"],
                $data["status_id"]
            ));
            $responce=[
                "status"=>true,
                "message"=>"user created"
            ];
            echo json_encode($responce);
           // var_dump($data); die();
           // exit();
            //$addedEmploye=$createEmploye->fetchAll();
        } catch (PDOException $e) {
             $responce=[
                "status"=>false,
                "message"=>"user not created"
            ];
            echo json_encode($responce);
        }

    }

    function importemploye($connect){
       if(move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
        $responce=[
            "status"=>false,
            "message"=>"file not found"
        ];
       }

         if (!file_exists('../files/'.$_FILES['Employers']['name'])) {
             $responce=[
                 "status"=>false,
                 "message"=>"file not found"
             ];
             echo json_encode($responce); die();
         }
        try{
            $file =fopen("../files/".$_FILES['Employers']['name'], 'r');     
            $counter =0;       
            while (!feof($file)){
                $employer = fgetcsv($file,1024 ,';');

                //print_r($employer);die();
                //$i = count($employer);
                if ($employer[1]!=null){
                    $createEmployee=$connect->prepare(
                        "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?)");
                       // print_r($employer);
                        //if(empty($data["middlename"])) $data["middlename"]="-";
                        $createEmployee->execute(array(
                            iconv("windows-1251","utf-8",strval($employer[0])), //firstName
                            iconv("windows-1251","utf-8",strval($employer[1])), //lastname
                            iconv("windows-1251","utf-8",strval($employer[2])), //middlename
                            strval($employer[3]),  //email
                            md5(strval($employer[4])), //password
                            strval($employer[5]), //inn
                            $employer[6], //post_id
                            $employer[7] //status_id
                        ));


                        //print_r($employer); die();
                        $counter++;

                }
            }
            $responce=[
                "status"=>true,
                "message"=>"$counter user imported"
            ];
            echo json_encode($responce);
        } catch(PDOException  $e){
            $responce=[
                "status"=>false,
                "message"=>"employers are not imported"
            ];
            echo json_encode($responce);
        }
        fclose($file);
      //  unlink('../files/'.$_FILES['Employers']['name']);
    }
